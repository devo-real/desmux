using System;
using static Cosmos.System.Power;

namespace Desmux.System.CRUSH.Core.SystemCommands
{
    public class ShutdownCommand : ICommand
    {
        public string Name => "shutdown";
        public string Desc => "Shuts down the system.";
        public void Execute(string[] args) => Shutdown();
    }
}