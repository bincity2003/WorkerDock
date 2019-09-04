using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WorkerDock.Plugins.Component.Private;
using Newtonsoft.Json;

namespace WorkerDock.Plugins
{
    public sealed class NotePlugin : PluginObject
    {
        public override string Name => "NotePlugin";
        public override string Version => "NotePlugin-1.0.0";
        public override string InvokeID => "note";
        public override string[] CallableCommand { get; }

        private Dictionary<string, Note> DataTable;

        public NotePlugin()
        {
            CallableCommand = new string[]
            {
                "add",
                "view",
                "delete",
                "version",
            };

            if (!File.Exists("Plugins/NotePlugin/usr/data.txt"))
            {
                DataTable = new Dictionary<string, Note>();
            }
            else
            {
                string text = File.ReadAllText("Plugins/NotePlugin/usr/data.txt");
                try
                {
                    DataTable = JsonConvert.DeserializeObject<Dictionary<string, Note>>(text);
                }
                catch
                {
                    DataTable = new Dictionary<string, Note>();
                }
            }
        }

        public override int Run(string command, string[] args)
        {
            if (args is null || command is null)
            {
                Console.WriteLine(File.ReadAllText("Plugins/NotePlugin/NoArgNoteCall.txt"));
                return -1;
            }

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
            bool includeDate = false;
            string content = string.Empty;
            if (args.Length >= 2)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-d" || args[i] == "--date")
                    {
                        includeDate = true;
                    }
                    else if (args[i].IndexOf("\"") == 0)
                    {
                        content = string.Join(' ', args[i..^0]);
                    }                   
                }
                content = content[1..^1];
                Note note = new Note();
                if (includeDate)
                {
                    note.Date = DateTime.UtcNow;
                }
                note.Content = content;
                string index = note.GetHashCode().ToString();
                note.Index = index;
                DataTable.Add(index, note);
            }
            else
            {
                if (args is null || args.Length == 0)
                {
                    Console.WriteLine(File.ReadAllText("Plugins/NotePlugin/InvalidArgAddCall.txt"));
                }
                else
                {
                    foreach (var item in args)
                    {
                        switch (item)
                        {
                            case "-d":
                            case "--date":
                                includeDate = true;
                                break;
                            default:
                                content = item;
                                break;
                        }
                    }

                    Note note = new Note();
                    if (includeDate)
                    {
                        note.Date = DateTime.UtcNow;
                    }
                    note.Content = content;
                    string index = note.GetHashCode().ToString();
                    note.Index = index;
                    DataTable.Add(index, note);
                }
            }                        
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
