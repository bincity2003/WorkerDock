using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkerDock.Plugins
{
    public sealed class NotePlugin : PluginObject
    {
        public override string Name => "NotePlugin";
        public override string Version => "NotePlugin-1.0.0";
        public override string InvokeID => "note";
        public override string[] CallableCommand { get; }

        public NotePlugin()
        {
            CallableCommand = new string[]
            {
                "add",
                "view",
                "delete",
                "version",
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
                case "version":
                    Console.WriteLine(Version);
                    break;
                case "add":
                    CallAdd(args);
                    break;
                case "delete":
                    CallDelete(args);
                    break;
                case "view":
                    CallView(args);
                    break;
            }

            return 0;
        }

        private void CallAdd(string[] args)
        {
            throw new NotImplementedException();
        }

        private void CallDelete(string[] args)
        {
            throw new NotImplementedException();
        }

        private void CallView(string[] args)
        {
            throw new NotImplementedException();
        }
        
        private void UnrecognizedCommand(string command)
        {
            Console.WriteLine($"\"{command}\" is not a valid command. Please check syntax!");
        }
    }
}
