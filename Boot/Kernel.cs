using Cosmos.Core.Memory;
using Cosmos.System.Graphics;
using Desmux.System.CRUSH.Core;
using System.Text;
using Sys = Cosmos.System;
using System.Drawing;
using Cosmos.System.Graphics.Fonts;
using Cosmos.System;
using System.IO;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;

namespace Desmux.Boot
{
    public class Kernel : Sys.Kernel
    {
        public static Canvas Canvas;
        public static GraphicalConsole GConsole;
        public static CosmosVFS vfs = new CosmosVFS();
        public static string CurrentDirectory { get; set; } = Directory.GetCurrentDirectory();
        private static CommandRegistry registry = new();

        public static string LoggedUser = "root";
        public static string PC_Name = "desmux-os";
        public static string OS_Name = "Desmux";
        public static string OS_Ver = "v1.0";
        public static string OS_Rev = "20250516";

        public static string Kernel_Name = "Descore";
        public static string Kernel_Ver = "v1.0";
        public static string Shell_Name = "crush";
        public static string Shell_Ver = "v1.0.00";

        public static uint CanvX = 1920;
        public static uint CanvY = 1080;
        
        public static string cpuBrand = Cosmos.Core.CPU.GetCPUBrandString();
        public static uint ramAmount = Cosmos.Core.CPU.GetAmountOfRAM();

        protected override void BeforeRun()
        {
            Canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(CanvX, CanvY, ColorDepth.ColorDepth32));
            Canvas.Clear(Color.Black);
            Canvas.Display();
            GConsole = new GraphicalConsole(Canvas);
            VFSManager.RegisterVFS(vfs);

            Directory.SetCurrentDirectory("0:\\");

            GConsole.Clear();
            GConsole.WriteLine("Welcome to Desmux!");
        }

        protected override void Run()
        {
            GConsole.SetForegroundColor(Color.ForestGreen);
            GConsole.Write($"{LoggedUser}@{PC_Name}");
            GConsole.SetForegroundColor(Color.DarkGray);
            GConsole.Write($"~{Directory.GetCurrentDirectory()}");
            GConsole.SetForegroundColor(Color.White);
            GConsole.Write($" $ ");
            string input = GConsole.ReadLine();

            CommandManager.Execute(input);
        }

        public static void Panic(string cause = "Kernel panic", string details = "No additional info")
        {
            int faultAddr = Heap.Collect();
            GConsole.Clear();

            string[] lines = new[]
            {
                $"[!!] {cause}",
                $"[**] Faulting address: 0x{faultAddr:X8}",
                $"[>>] Details: {details}",
                "",
                $"[##] System: {OS_Name} {OS_Ver} ({OS_Rev})",
                $"[<>] Kernel: {Kernel_Name} {Kernel_Ver}",
                "",
                "[!!] The system has halted. Hold the power cycle or reset button on your device."
            };

            foreach (var line in lines)
            {
                GConsole.WriteLine(line);
            }
            
            // An actual halt
            Cosmos.Core.CPU.DisableInterrupts();
        }
    }

    public class GraphicalConsole
    {
        private Canvas canvas;
        private int x = 0, y = 0;
        private int charWidth = 8;
        private int charHeight = 16;
        private int maxCols;
        private int maxRows;
        private Color foreground = Color.White;
        private Color background = Color.Black;
        private PCScreenFont font = PCScreenFont.Default;

        // Made these instance fields instead of static
        private int CursorX = 0;
        private int CursorY = 0;
        private int LineHeight = 16;
        private int CharWidth = 8;

        // Store default colors
        private readonly Color defaultForeground = Color.White;
        private readonly Color defaultBackground = Color.Black;

        public GraphicalConsole(Canvas canvas)
        {
            this.canvas = canvas;
            maxCols = (int)canvas.Mode.Width / charWidth;
            maxRows = (int)canvas.Mode.Height / charHeight;
            Clear();
        }

        // Color control methods
        public void SetForegroundColor(Color color)
        {
            foreground = color;
        }

        public void SetBackgroundColor(Color color)
        {
            background = color;
        }

        public void ResetColor()
        {
            foreground = defaultForeground;
            background = defaultBackground;
        }

        public string ReadLine()
        {
            StringBuilder input = new();
            int initialX = x;
            int initialY = y;

            while (true)
            {
                if (KeyboardManager.TryReadKey(out var key))
                {
                    if (key.Key == ConsoleKeyEx.Enter)
                    {
                        WriteLine();
                        break;
                    }
                    else if (key.Key == ConsoleKeyEx.Backspace)
                    {
                        if (input.Length > 0)
                        {
                            input.Remove(input.Length - 1, 1);
                            x--;
                            if (x < initialX && y > initialY)
                            {
                                y--;
                                x = maxCols - 1;
                            }
                            else if (x < initialX)
                            {
                                x = initialX;
                            }
                            canvas.DrawFilledRectangle(background, x * charWidth, y * charHeight, charWidth, charHeight);
                            canvas.Display();
                        }
                    }
                    else if (key.KeyChar != '\0')
                    {
                        char c = key.KeyChar;
                        input.Append(c);
                        Write(c.ToString());
                    }
                }
            }
            return input.ToString();
        }

        public void Write(string text)
{
    int i = 0;
    while (i < text.Length)
    {
        char c = text[i];

        if (c == '\x1B' && i + 1 < text.Length && text[i + 1] == '[')
        {
            int seqStart = i + 2;
            int seqEnd = text.IndexOf('m', seqStart);

            if (seqEnd != -1)
            {
                string code = text.Substring(seqStart, seqEnd - seqStart);

                if (int.TryParse(code, out int colorCode))
                {
                    foreground = colorCode switch
                    {
                        30 => Color.Black,
                        31 => Color.DarkRed,
                        32 => Color.DarkGreen,
                        33 => Color.Olive,
                        34 => Color.DarkBlue,
                        35 => Color.DarkMagenta,
                        36 => Color.DarkCyan,
                        37 => Color.LightGray,
                        90 => Color.Gray,
                        91 => Color.Red,
                        92 => Color.Green,
                        93 => Color.Yellow,
                        94 => Color.Blue,
                        95 => Color.Magenta,
                        96 => Color.Cyan,
                        97 => Color.White,
                        _ => foreground
                    };
                }

                i = seqEnd + 1;
                continue;
            }
        }

        if (c == '\n')
        {
            NewLine();
            i++;
            continue;
        }

        if (c == '\b')
        {
            foreground = Color.Blue;
            i++;
            continue;
        }

        if (c == '\r')
        {
            foreground = Color.Red;
            i++;
            continue;
        }

        if (c == '\t')
        {
            foreground = Color.Green;
            i++;
            continue;
        }

        if (c == '\f')
        {
            foreground = Color.White;
            i++;
            continue;
        }

        canvas.DrawString(c.ToString(), font, foreground, x * charWidth, y * charHeight);
        x++;
        CursorX = x * charWidth;
        CursorY = y * charHeight;

        if (x >= maxCols)
        {
            NewLine();
        }

        i++;
    }

    canvas.Display();
}



        public void WriteLine()
        {
            NewLine();
        }

        public void WriteLine(string text)
        {
            Write(text);
            NewLine();
        }

        public void Clear()
        {
            canvas.Clear(background);
            x = 0;
            y = 0;
            CursorX = 0;
            CursorY = 0;
            canvas.Display();
        }

        public void OK()
        {
            
        }

        public void ErrorMsg(string failpoint, string errorMsg)
        {
            Write($"{failpoint}: ");
            SetForegroundColor(Color.IndianRed);
            Write(errorMsg + "\n");
            ResetColor();
        }

        public void WarnMsg(string warnPoint, string msg)
        {
            Write($"{warnPoint}: ");
            SetForegroundColor(Color.IndianRed);
            Write(msg + "\n");
            ResetColor();
        }

        private void NewLine()
        {
            x = 0;
            y++;
            CursorX = 0;
            CursorY += LineHeight;

            if (y >= maxRows)
            {
                Clear();
            }
        }
    }
}
