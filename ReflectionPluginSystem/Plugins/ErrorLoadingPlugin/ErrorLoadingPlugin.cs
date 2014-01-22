using ReflectionPluginSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorLoadingPlugin
{
    public class ErrorLoadingPlugin : IPluginInterface
    {
        public string Name
        {
            get { return "Invalid plugin"; }
        }

        public string Description
        {
            get { return "This plugin will not load"; }
        }

        public Version Version
        {
            get { return new Version(1, 0); }
        }

        public string GetMessage()
        {
            return "This string will not display.";
        }
    }
}
