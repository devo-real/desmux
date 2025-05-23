using System;
using System.IO;

namespace Desmux.System.CRUSH.Core.FileSystemCommands
{
    public class MkdirCommand : ICommand
    {
        public string Name => "mkdir";
        public string Desc => "Creates a new directory.";
        public void Execute(string[] args)
        {
            if (args.Length == 0) return;
            Directory.CreateDirectory(args[0]);
        }
    }
}