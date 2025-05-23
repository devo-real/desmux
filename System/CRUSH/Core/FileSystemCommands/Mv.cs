using System;
using System.IO;

namespace Desmux.System.CRUSH.Core.FileSystemCommands
{
    public class MvCommand : ICommand
    {
        public string Name => "mv";
        public string Desc => "Moves or renames a file.";
        public void Execute(string[] args)
        {
            if (args.Length < 2) return;
            File.Create(args[1]);
            File.WriteAllText(File.ReadAllText(args[0]), args[1]);
            File.Delete(args[0]);
        }
    }
}