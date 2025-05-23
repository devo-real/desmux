using static Desmux.Boot.Kernel;

namespace Desmux.System.CRUSH.Core.SystemCommands
{
    public class PanicCommand : ICommand
    {
        public string Name => "panic";
        public string Desc => "Triggers a kernel panic simulation.";
        public void Execute(string[] args) => Panic("User-initiated kernel panic.", "The end-user ran the 'panic' command. The system has been halted.");
    }
}