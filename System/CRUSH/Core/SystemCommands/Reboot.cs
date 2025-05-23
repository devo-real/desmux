using System;
using static Cosmos.System.Power;

namespace Desmux.System.CRUSH.Core.SystemCommands
{
    public class RebootCommand : ICommand
    {
        public string Name => "reboot";
        public string Desc => "Reboots the system.";
        public void Execute(string[] args) => Reboot();
    }
}