using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ReflectionPluginSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ReflectionPluginSystem.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private const string PluginsFolder = "Plugins";

        private ObservableCollection<IPluginInterface> plugins;

        public ObservableCollection<IPluginInterface> Plugins
        {
            get
            {
                return this.plugins;
            }
            private set
            {
                if (this.plugins != value)
                {
                    this.plugins = value;
                    this.RaisePropertyChanged(() => this.Plugins);
                }
            }
        }

        private IPluginInterface selectedPlugin;
        public IPluginInterface SelectedPlugin
        {
            get
            {
                return this.selectedPlugin;
            }
            set
            {
                if (this.selectedPlugin != value)
                {
                    this.selectedPlugin = value;
                    this.RaisePropertyChanged(() => this.SelectedPlugin);
                }
            }
        }

        private string applicationLog;
        public string ApplicationLog
        {
            get
            {
                return this.applicationLog;
            }
            private set
            {
                if (this.applicationLog != value)
                {
                    this.applicationLog = value;
                    this.RaisePropertyChanged(() => this.ApplicationLog);
                }
            }
        }

        private RelayCommand showMessageCommand;
        public RelayCommand ShowMessageCommand
        {
            get
            {
                if (this.showMessageCommand == null)
                {
                    this.showMessageCommand = new RelayCommand(() => this.LogMessage(this.SelectedPlugin.GetMessage()), () => this.SelectedPlugin != null);
                }

                return this.showMessageCommand;
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            this.LoadPlugins();
        }

        private void LoadPlugins()
        {
            ObservableCollection<IPluginInterface> loadedPlugins = new ObservableCollection<IPluginInterface>();

            string pluginsPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, PluginsFolder);
            string[] pluginsFilename = Directory.GetFiles(pluginsPath, "*.dll");

            foreach (string pluginLibraryPath in pluginsFilename)
            {
                Assembly pluginAssembly = Assembly.LoadFrom(pluginLibraryPath);

                if (pluginAssembly != null)
                {
                    List<Type> exportedTypes = pluginAssembly
                        .GetExportedTypes()
                        .Where(type =>
                            type.GetInterfaces().Contains(typeof(IPluginInterface))
                            && type.IsClass
                            && !type.IsAbstract)
                        .ToList();

                    foreach (Type exportedType in exportedTypes)
                    {
                        IPluginInterface newPlugin = null; 

                        try
                        {
                            newPlugin = (IPluginInterface)Activator.CreateInstance(exportedType);
                            loadedPlugins.Add(newPlugin);
                            this.LogMessage("Successfully loaded \"" + newPlugin.ToString() + "\"");
                        }
                        catch (Exception e)
                        {
                            this.LogMessage("Error loading type \"" + exportedType.FullName + "\": " + e.Message);
                        }
                    }
                }
            }

            this.Plugins = loadedPlugins;
        }

        private void LogMessage(string message)
        {
            this.ApplicationLog = this.ApplicationLog + message + "\n";
        }
    }
}