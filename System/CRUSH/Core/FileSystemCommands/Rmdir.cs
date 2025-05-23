using static Desmux.Boot.Kernel;
using System.IO;

namespace Desmux.System.CRUSH.Core.FileSystemCommands
{
    public class RmdirCommand : ICommand
    {
        public string Name => "rmdir";
        public string Desc => "Removes a directory.";
        public void Execute(string[] args)
        {
            if (args.Length == 0) return;
            var dir = args[0];
            if (Directory.Exists(dir)) Directory.Delete(dir, true);
            else GConsole.WriteLine($"Directory not found: {dir}");
        }
    }
}