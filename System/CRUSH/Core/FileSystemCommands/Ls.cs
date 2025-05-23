using System;
using System.IO;
using static Desmux.Boot.Kernel;

namespace Desmux.System.CRUSH.Core.FileSystemCommands
{
    public class LsCommand : ICommand
    {
        public string Name => "ls";
        public string Desc => "Lists files in the current directory.";
        public void Execute(string[] args)
        {
            var path = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
            foreach (var file in Directory.GetFiles(path)) GConsole.WriteLine(Path.GetFileName(file));
            foreach (var dir in Directory.GetDirectories(path)) GConsole.WriteLine($"{Path.GetFileName(dir)}/");
        }
    }
}