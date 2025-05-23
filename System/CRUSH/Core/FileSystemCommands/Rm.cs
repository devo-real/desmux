using System;
using System.IO;

namespace Desmux.System.CRUSH.Core.FileSystemCommands
{
    public class RmCommand : ICommand
    {
        public string Name => "rm";
        public string Desc => "Deletes a file.";
        public void Execute(string[] args)
        {
            if (args.Length == 0) return;
            var file = args[0];
            if (File.Exists(file)) File.Delete(file);
            else Desmux.Boot.Kernel.GConsole.WriteLine($"File not found: {file}");
        }
    }
}