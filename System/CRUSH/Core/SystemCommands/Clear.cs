using System;
using Desmux.Boot;

namespace Desmux.System.CRUSH.Core.SystemCommands
{
    public class ClearCommand : ICommand
    {
        public string Name => "clear";
        public string Desc => "Clears the screen.";
        public void Execute(string[] args) => Kernel.GConsole.Clear();
    }
}