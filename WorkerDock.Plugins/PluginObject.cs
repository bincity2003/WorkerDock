using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerDock.Plugins
{
    public abstract class PluginObject
    {
        public abstract string Name { get; }
        public abstract string Version { get; }
        public abstract string InvokeID { get; }
        public abstract string[] CallableCommand { get; }

        public abstract int Run(string name, string[] args);
    }
}
