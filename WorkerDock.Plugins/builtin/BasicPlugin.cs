using System;
using System.Collections.Generic;
using System.Linq;
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

        public override int Run(string command, string[] args)
        {
            command = command.ToLower();

            if (!CallableCommand.Contains(command))
            {
                UnrecognizedCommand(command);
            }

            switch (command)
            {
                default:
                    break;
            }
        }

        private void UnrecognizedCommand(string command)
        {
            Console.WriteLine($"\"{command}\" is not a valid command. Please check syntax!");
        }
    }
}
