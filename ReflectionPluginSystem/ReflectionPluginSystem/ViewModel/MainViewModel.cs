using GalaSoft.MvvmLight;
using ReflectionPluginSystem.Interfaces;
using System.Collections.ObjectModel;

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

        public string ApplicationLog
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }
    }
}