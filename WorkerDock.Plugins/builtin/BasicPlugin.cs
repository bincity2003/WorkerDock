using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerDock.Plugins.builtin
{
    public sealed class BasicPlugin : PluginObject
    {
        public override string Name => "BasicPlugin";
        public override string Version => "1.0.0";
        public override string InvokeID => "";
        public override string[] CallableCommand { get; }

        public BasicPlugin()
        {
            CallableCommand = new string[]
            {
                "exit",
                "clean",
                "config"
            };
        }

        public override void Run(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
