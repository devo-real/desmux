using System.Drawing;
using System.Linq;
using static Desmux.Boot.Kernel;

namespace Desmux.System.CRUSH.Core.SystemCommands
{
    public class NeofetchCmd : ICommand
    {
        // This is fugly af but i have to do it or COSMOS will scream at me
        private static string blockChars1 = "\x1B[30m\x1B[31m¤¤\x1B[32m¤¤\x1B[33m¤¤\x1B[34m¤¤\x1B[35m¤¤\x1B[36m¤¤\x1B[37m¤¤";
        private static string blockChars2 = "\x1B[90m¤¤\x1B[91m¤¤\x1B[92m¤¤\x1B[93m¤¤\x1B[94m¤¤\x1B[95m¤¤\x1B[96m¤¤\x1B[97m¤¤";
        
        private string[] asciiArt = new[]
        {
            "\b      .@. .@       .@.@\b      ",
            "\b#           .          .\b     ",
            "#@        \f.. .\b         .     ",
            " @. \f...     ...:..% %.\b  @    ",
            " .#           \f...\b       .    ",
            "\b  *@          .....   ..@.\b   ",
            "\b   @..@.       .@@*          \b",
            "\b   .#.\b                       ",
            "\b    -@\b                       ",
            "\b     @.\b                      ",
            "\b     .#.\b                     ",
            "\b      :@\b                     ",
            "\b       %\b                    ",
            "\b        @\b                   "
        };

        private string[] info = new[]
        {
            $"{LoggedUser}@{PC_Name}",
            $"OS: {OS_Name} {OS_Ver}",
            $"OS revision: {OS_Rev}",
            $"Kernel: {Kernel_Name} {Kernel_Ver}",
            $"Shell: {Shell_Name} {Shell_Ver}",
            $"CPU: {cpuBrand}",
            $"RAM: {ramAmount}MB",
            $"Resolution: {CanvX}x{CanvY}",
            $"Terminal: Cosmos Inbuilt Graphical Canvas",
            "",
            blockChars1,
            blockChars2
        };

        public string Name => "neofetch";
        public string Desc => "Display system info";

        public void Execute(string[] args)
        {
            var asciiColor = Color.Blue;
            GConsole.WriteLine("\n");

            int artInfoGap = 3;
            int asciiWidth = asciiArt.Max(line => line.Length);
            int infoStart = asciiWidth + artInfoGap;

            for (int i = 0; i < asciiArt.Length; i++)
            {
                GConsole.SetForegroundColor(asciiColor);
                GConsole.Write(" " + asciiArt[i]);

                if (i < info.Length)
                {
                    GConsole.SetForegroundColor(Color.White);
                    GConsole.Write(new string(' ', infoStart - asciiArt[i].Length));
                    GConsole.Write(info[i] + "\n");
                }
                else
                {
                    GConsole.Write("\n");
                }
            }

            GConsole.WriteLine("\n");
            GConsole.SetForegroundColor(Color.White);
        }
    }
}
