﻿using System;
using System.Collections.Generic;
using WorkerDock.Plugins;

namespace WorkerDock
{
    internal class Program
    {
        private static string User;
        private static string Domain;
        private static string Prompt => $"{User}@{Domain} > ";

        private static Dictionary<string, PluginObject> Plugins;

        private static void Main(string[] args)
        {
            PreStartupSetup();

            bool executionContinue = true;
            string[] command;
            string callee;
            string[] parameters;
            while (executionContinue)
            {
                Console.Write(Prompt);
                command = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (command.Length == 0)
                {
                    continue;
                }
                callee = command[0];
                parameters = command[1..^0];

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
            // Prompt setting
            User = "user";
            Domain = "SPMC";

            // Load plugins
            Plugins = new Dictionary<string, PluginObject>();

            BasicPlugin basic = new BasicPlugin();
            Plugins.Add(basic.InvokeID, basic);
        }
    }
}
