using System;
using System.IO;

using Emulator.Core;
using Emulator.Core.Interfaces.Units;
using Emulator.Core.Utils;

namespace Emulator.ConsoleTestApp
{
    class Program
    {
        static System.Text.StringBuilder _displayContent = new System.Text.StringBuilder();
        static char[] CharMap = { ' ', '\u2588' };

        static DisplayBuffer _display = new DisplayBuffer(64, 32);

        static void Main(string[] args)
        {
            try
            {
                IChip8Device device = BuildDevice();

                LoadProgram(device, "program");

                int displayContentVersion = _display.ContentVersion;

                while (true)
                {
                    device.Tick();
                    if (displayContentVersion != _display.ContentVersion)
                    {
                        DrawScreen(_display);
                        displayContentVersion = _display.ContentVersion;
                    }
                    // System.Threading.Thread.Sleep(100);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Caught an exception: {e.Message}");
            }
        }

        static IChip8Device BuildDevice()
        {
            using (var builder = new Chip8DeviceBuilder())
                return builder.Build(new KeyboardStub(), _display);
        }

        static void LoadProgram(IChip8Device device, string programPath)
        {
            using (var stream = new FileStream("program", FileMode.Open))
                using (var loader = new ProgramLoader(device))
                    loader.Load(stream);
        }

        static void DrawScreen(DisplayBuffer display)
        {
            int offset = 0;
            _displayContent.Clear();
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < display.Height; i++)
            {
                for (int j = 0; j < display.Width; j++)
                    _displayContent.Append(CharMap[display[offset++]]);
                _displayContent.Append('\n');
            }
            _displayContent.Append("\n\n");
            Console.Write(_displayContent.ToString());
        }
    }
}
