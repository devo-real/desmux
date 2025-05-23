using System;
using System.IO;

namespace Desmux.System.CRUSH.Core.FileSystemCommands
{
    public class CopyCommand : ICommand
    {
        public string Name => "copy";
        public string Desc => "Copies a file.";
        public void Execute(string[] args)
        {
            if (args.Length < 2) return;
            File.Create(args[1]);
            File.WriteAllText(File.ReadAllText(args[0]), args[1]);
        }
    }
}