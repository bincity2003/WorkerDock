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

        public abstract void Startup(string[] args);
    }
}
