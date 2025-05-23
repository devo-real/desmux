using System.IO;

namespace Desmux.System.CRUSH.Core.FileSystemCommands
{
    public class CdCommand : ICommand
    {
        public string Name => "cd";
        public string Desc => "Changes the current directory.";
        public void Execute(string[] args)
        {
            if (args.Length == 0) return;
            var target = args[0];
            if (Directory.Exists(target)) Directory.SetCurrentDirectory(target);
            else Desmux.Boot.Kernel.GConsole.WriteLine($"Directory not found: {target}");
        }
    }
}