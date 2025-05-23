using System;
using Cosmos.Core;
using Cosmos.Core.Memory;
using static Desmux.Boot.Kernel;

namespace Desmux.System.CRUSH.Core.DebugCommands
{
    public class AddrInfo : ICommand
    {
        public string Name => "addrinfo";
        public string Desc => "Echoes the heap address and current memory address";

        public void Execute(string[] args)
        {
            try
            {
                GConsole.WriteLine($"HEAP: {Heap.Collect():x8}");
                GConsole.WriteLine($"ADDR: {GCImplementation.GetSafePointer(new object()):x8}");
            }
            catch (Exception ex)
            {
                GConsole.WriteLine("addrinfo: " + ex.Message);
            }
        }
    }
}