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

        private string _Name = "user";
        private string _Domain = "WorkerDock";
        private bool _IsDomainShown = true;
        private bool _IsPromptShown = true;
        public string Prompt
        {
            get
            {
                if (!_IsPromptShown)
                {
                    return "> ";
                }
                else
                {
                    if (_IsDomainShown)
                    {
                        return $"{_Name}@{_Domain}> ";
                    }
                    else
                    {
                        return $"{_Name}> ";
                    }
                }
            }
        }

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
            if (args.Length != 1)
            {
                Console.WriteLine(System.IO.File.ReadAllText("Plugins/BasicPlugin/InvalidArgConfigCall.txt"));
            }
            else
            {
                string[] pair = args[0].Split(':', StringSplitOptions.RemoveEmptyEntries);
                if (pair.Length == 2)
                {
                    string name = pair[0];
                    string value = pair[1];

                    switch (name)
                    {
                        case "user":
                            _Name = value;
                            break;
                        case "domain":
                            _Domain = value;
                            break;
                        case "isDomainShown":
                            bool status;
                            if (!bool.TryParse(value, out status))
                            {
                                Console.WriteLine("Invalid value given!");
                            }
                            else
                            {
                                _IsDomainShown = status;
                            }
                            break;
                        case "isPromptShown":
                            if (!bool.TryParse(value, out status))
                            {
                                Console.WriteLine("Invalid value given!");
                            }
                            else
                            {
                                _IsPromptShown = status;
                            }
                            break;
                        default:
                            Console.WriteLine(System.IO.File.ReadAllText("Plugins/BasicPlugin/InvalidArgConfigCall.txt"));
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(System.IO.File.ReadAllText("Plugins/BasicPlugin/InvalidArgConfigCall.txt"));
                }
            }
        }

        private void UnrecognizedCommand(string command)
        {
            Console.WriteLine($"\"{command}\" is not a valid command. Please check syntax!");
        }
    }
}
