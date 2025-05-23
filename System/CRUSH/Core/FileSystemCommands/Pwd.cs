using static Desmux.Boot.Kernel;
using System.IO;

namespace Desmux.System.CRUSH.Core.FileSystemCommands
{
    public class PwdCommand : ICommand
    {
        public string Name => "pwd";
        public string Desc => "Prints the current working directory.";
        public void Execute(string[] args) => GConsole.WriteLine(Directory.GetCurrentDirectory());
    }
}