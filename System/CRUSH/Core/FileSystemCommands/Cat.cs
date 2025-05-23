using static Desmux.Boot.Kernel;
using System.IO;

namespace Desmux.System.CRUSH.Core.FileSystemCommands
{
    public class CatCommand : ICommand
    {
        public string Name => "cat";
        public string Desc => "Displays contents of a file.";
        public void Execute(string[] args)
        {
            if (args.Length == 0) return;
            var file = args[0];
            if (File.Exists(file)) GConsole.WriteLine(File.ReadAllText(file));
            else GConsole.WriteLine($"File not found: {file}");
        }
    }
}