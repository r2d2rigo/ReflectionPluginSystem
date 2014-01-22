using ReflectionPluginSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldPlugin
{
    public class HelloWorldPlugin : IPluginInterface
    {
        public string Name
        {
            get { return "Hello World plugin"; }
        }

        public string Description
        {
            get { return "A plugin that shows the \"Hello World!\" message"; }
        }

        public Version Version
        {
            get { return new Version(1, 5); }
        }

        public string GetMessage()
        {
            return "Hello, World!";
        }
    }
}
