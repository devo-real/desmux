using Desmux.Boot;

namespace Desmux.System.CRUSH.Core.SystemCommands
{
    public class EchoCommand : ICommand
    {
        public string Name => "echo";
        public string Desc => "Echoes input text.";
        public void Execute(string[] args) => Kernel.GConsole.WriteLine(string.Join(" ", args));
    }
}