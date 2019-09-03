using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkerDock.Plugins
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
                "config"
            };
        }

        public override int Run(string command, string[] args)
        {
            command = command.ToLower();

            if (!CallableCommand.Contains(command))
            {
                UnrecognizedCommand(command);
                return -1;
            }

            switch (command)
            {
                case "exit":
                    CallExit();
                    break;
                case "config":
                    CallConfig(args);
                    break;
            }

            return 0;
        }       

        private void CallExit()
        {
            Environment.Exit(0);
        }

        private void CallConfig(string[] args)
        {

        }

        private void UnrecognizedCommand(string command)
        {
            Console.WriteLine($"\"{command}\" is not a valid command. Please check syntax!");
        }
    }
}
