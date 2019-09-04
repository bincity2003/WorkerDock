using System;
using System.Linq;

namespace WorkerDock.Plugins
{   
    public sealed class BasicPlugin : PluginObject
    {
        public override string Name => "BasicPlugin";
        public override string Version => "BasicPlugin-1.0.0";
        public override string InvokeID => "";
        public override string[] CallableCommand { get; }

        public delegate void CustomHandler();
        public event CustomHandler OnPromptChange;

        private string _User = "user";
        private string _Domain = "WorkerDock";
        private bool _IsDomainShown = true;
        private bool _IsPromptShown = true;

        public string User
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
                OnPromptChange?.Invoke();
            }
        }
        public string Domain
        {
            get
            {
                return _Domain;
            }
            set
            {
                _Domain = value;
                OnPromptChange?.Invoke();
            }
        }
        public bool ShowDomain
        {
            get
            {
                return _IsDomainShown;
            }
            set
            {
                _IsDomainShown = value;
                OnPromptChange?.Invoke();
            }
        }
        public bool ShowPrompt
        {
            get
            {
                return _IsPromptShown;
            }
            set
            {
                _IsPromptShown = value;
                OnPromptChange?.Invoke();
            }
        }
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
                        return $"{_User}@{_Domain}> ";
                    }
                    else
                    {
                        return $"{_User}> ";
                    }
                }
            }
        }

        public BasicPlugin()
        {
            CallableCommand = new string[]
            {
                "config",
                "clear",
                "exit",
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
                case "clear":
                    CallClear();
                    break;
                case "exit":
                    CallExit();
                    break;
                case "config":
                    CallConfig(args);
                    break;
            }

            return 0;
        }

        private void CallClear()
        {
            Console.Clear();
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
                    string name = pair[0].ToLower();
                    string value = pair[1];

                    switch (name)
                    {
                        case "user":
                            User = value;
                            break;
                        case "domain":
                            Domain = value;
                            break;
                        case "isdomainshown":
                            bool status;
                            if (!bool.TryParse(value, out status))
                            {
                                Console.WriteLine("Invalid value given!");
                            }
                            else
                            {
                                ShowDomain = status;
                            }
                            break;
                        case "ispromptshown":
                            if (!bool.TryParse(value, out status))
                            {
                                Console.WriteLine("Invalid value given!");
                            }
                            else
                            {
                                ShowPrompt = status;
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
