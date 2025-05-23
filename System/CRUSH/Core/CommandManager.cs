using Desmux.System.CRUSH.Core;
using System;
using System.Collections.Generic;
using Desmux.Boot;

namespace Desmux.System.CRUSH.Core
{
    public static class CommandManager
    {
        private static readonly Dictionary<string, ICommand> Commands = new();

        public static void Register(ICommand command)
        {
            if (!Commands.ContainsKey(command.Name.ToLower()))
                Commands.Add(command.Name.ToLower(), command);
        }

        public static void Execute(string input)
        {
            try
            {
                var parts = input.Trim().Split(' ');
                if (parts.Length == 0 || string.IsNullOrWhiteSpace(parts[0])) return;

                var cmdName = parts[0].ToLower();

                string[] args = new string[Math.Max(0, parts.Length - 1)];
                for (int i = 1; i < parts.Length; i++)
                {
                    args[i - 1] = parts[i];
                }

                if (Commands.TryGetValue(cmdName, out var command))
                {
                    command.Execute(args);
                }
                else
                {
                    Kernel.GConsole.WriteLine($"Unknown command: {cmdName}");
                }
            }
            catch (Exception err)
            {
                Kernel.GConsole.WriteLine("crush: " + err.Message);
            }
        }

        public static void ListCommands()
        {
            const int pageSize = 25;
            var commandEntries = new List<KeyValuePair<string, ICommand>>(Commands);
            int total = commandEntries.Count;
            int index = 0;

            while (index < total)
            {
                Kernel.GConsole.Clear();
                Kernel.GConsole.WriteLine("=== Registered commands ===\n");

                for (int i = index; i < Math.Min(index + pageSize, total); i++)
                {
                    var entry = commandEntries[i];
                    string name = entry.Key;
                    string description = entry.Value is not null && !string.IsNullOrWhiteSpace(entry.Value.Desc)
                        ? entry.Value.Desc
                        : "No description.";
                    Kernel.GConsole.WriteLine($"- {name.PadRight(35)} : {description}");
                }

                index += pageSize;

                if (index < total)
                {
                    Kernel.GConsole.WriteLine("\nPress Enter to see more...");
                    ReadLineBlock();
                }
                else
                {
                    Kernel.GConsole.WriteLine("\nEnd of command list.");
                }
            }
        }

        private static void ReadLineBlock()
        {
            while (true)
            {
                var key = Cosmos.System.KeyboardManager.ReadKey();
                if (key.Key == Cosmos.System.ConsoleKeyEx.Enter)
                    break;
            }
        }
    }
}