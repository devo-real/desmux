using System;
using System.IO;

namespace Desmux.System.CRUSH.Core.FileSystemCommands
{
    public class TouchCommand : ICommand
    {
        public string Name => "touch";
        public string Desc => "Creates an empty file.";
        public void Execute(string[] args)
        {
            if (args.Length == 0) return;
            File.Create(args[0]).Close();
        }
    }
}