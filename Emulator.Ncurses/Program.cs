using System;
using System.IO;
using System.Linq;

using Mindmagma.Curses;

using Emulator.Core;
using Emulator.Core.Interfaces.Units;
using Emulator.Core.Utils;

namespace Emulator.App.Curses
{
    class Program
    {
        const Byte DefaultWidth = 64;
        const Byte DefaultHeight = 32;

        static void Main(string[] args)
        {
            short[] colorMap = new short[] { 1, 2 };
            IntPtr screen = default;
            NCursesDisplay display = null;
            IChip8Device device = null;
    
            Func<bool>[] initializers =
            {
                () => InitializeNCurses(ref screen, colorMap, DefaultWidth, DefaultHeight),
                () => InitializeDisplayBuffer(ref display, screen, colorMap),
                () => BuildDevice(ref device, display.Buffer),
                () => LoadProgram(device, "program")
            };

            try
            {
                if (initializers.All(p => p()))
                    Run(device, display);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Caught an exception: {e.Message}");
            }

            NCurses.EndWin();
        }

        static bool BuildDevice(ref IChip8Device device, DisplayBuffer buffer)
        {
            using (var builder = new Chip8DeviceBuilder())
                device = builder.Build(new KeyboardStub(), buffer);
            return true;
        }

        static bool LoadProgram(IChip8Device device, string programPath)
        {
            using (var stream = new FileStream(programPath, FileMode.Open))
                using (var loader = new ProgramLoader(device))
                    loader.Load(stream);

            return true;
        }

        private static bool InitializeNCurses(ref IntPtr screen, short[] colorMap, Byte width, Byte height)
        {
            screen = NCurses.InitScreen();

            if (NCurses.HasColors() == false)
            {
                Console.WriteLine("Your terminal doesn't support colored output.");
                NCurses.EndWin();
                return false;
            }

            if ((NCurses.Columns < width - 1) || (NCurses.Lines < height - 1))
            {
                Console.WriteLine($"Your terminal must be {width}x{height}. You have {NCurses.Columns}x{NCurses.Lines}");
                NCurses.EndWin();
                return false;
            }

            NCurses.NoDelay(screen, true);
            NCurses.NoEcho();
            NCurses.AttributeSet(CursesAttribute.NORMAL);
            NCurses.StartColor();
            NCurses.InitPair(colorMap[0], CursesColor.BLACK, CursesColor.BLACK);
            NCurses.InitPair(colorMap[1], CursesColor.WHITE, CursesColor.WHITE);

            return true;
        }

        private static bool InitializeDisplayBuffer(ref NCursesDisplay display, IntPtr screen, short[] colorMap)
        {
            display = new NCursesDisplay(DefaultWidth, DefaultHeight, screen, colorMap);
            return true;
        }

        private static void Run(IChip8Device device, NCursesDisplay display)
        {
            while (true)
            {
                device.Tick();
                display.Draw();
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
