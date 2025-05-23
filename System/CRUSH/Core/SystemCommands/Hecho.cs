using System;
using System.Text;
using static Desmux.Boot.Kernel;

namespace Desmux.System.CRUSH.Core.DebugCommands
{
    public class HechoCommand : ICommand
    {
        public string Name => "hecho";
        public string Desc => "Echoes hexadecimal values of each input word.";

        public void Execute(string[] args)
        {
            try
            {
                foreach (var word in args)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(word);
                    foreach (var b in bytes)
                    {
                        GConsole.Write($"{b:X2} ");
                    }
                    GConsole.Write("  ");
                }
                GConsole.WriteLine();
            }
            catch (Exception ex)
            {
                GConsole.WriteLine($"hecho: {ex.Message}");
            }
        }
    }
}