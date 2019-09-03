using System;
using System.Collections.Generic;
using WorkerDock.Plugins;

namespace WorkerDock
{
    internal class Program
    {
        private static string Prompt;
        private static readonly Dictionary<string, PluginObject> Plugins = new Dictionary<string, PluginObject>();

        private static void Main(string[] args)
        {
            PreStartupSetup();

            bool executionContinue = true;
            string[] command;
            string callee;
            string[] parameters;
            while (executionContinue)
            {
                LoadPrompt();
                Console.Write(Prompt);
                command = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (command.Length == 0)
                {
                    continue;
                }
                callee = command[0];
                parameters = command[1..^0];

                // Checking version
                if (parameters.Length == 1 && parameters[0] == "--version")
                {
                    try
                    {
                        Console.WriteLine(Plugins[callee].Version);
                        continue;
                    }
                    catch (KeyNotFoundException)
                    {
                        Plugins[""].Run(callee, parameters);
                        continue;
                    }
                }

                // Parsing command
                try
                {
                    Plugins[callee].Run(parameters[0], parameters[1..^0]);
                }
                catch (KeyNotFoundException)
                {
                    Plugins[""].Run(callee, parameters);
                }
            }
        }

        private static void PreStartupSetup()
        {
            BasicPlugin basic = new BasicPlugin();
            NotePlugin note = new NotePlugin();

            Plugins.Add(basic.InvokeID, basic);
            Plugins.Add(note.InvokeID, note);

            // Load prompt
            LoadPrompt();
        }

        private static void LoadPrompt()
        {
            Prompt = ((BasicPlugin)Plugins[""]).Prompt;
        }
    }
}
