using System;

using Mindmagma.Curses;

using Emulator.Core;
using Emulator.Core.Interfaces.Periphery;

namespace Emulator.App.Curses
{
    public class KeyboardStub : IKeyboard
    {
        public bool IsKeyPressed(Byte index) => false;
        public void KeyPressWait(KeyPressedDelegate callback) {}

        public void KeyDown(Byte index) {}
        public void KeyUp(Byte index) {}

        public void Tick() {}
    }

    public class NCursesDisplay
    {
        private const int DefaultFramerate = 150;
        private IntPtr _ncursesWindow;
        private int _lastGeneration;
        private short[] _colorMap;

        public DisplayBuffer Buffer { get; }
        
        public NCursesDisplay(Byte width, Byte height, IntPtr ncursesWindow, short[] colorMap)
        {
            Buffer = new DisplayBuffer(width, height);
            _ncursesWindow = ncursesWindow;
            _lastGeneration = 0;
            _colorMap = colorMap;
            _lastGeneration = Buffer.ContentVersion;
        }

        public void Draw()
        {
            if (_lastGeneration != Buffer.ContentVersion)
            {
                _lastGeneration = Buffer.ContentVersion;
                UpdateScreenData();
            }
        }

        private void UpdateScreenData()
        {
            int offset = 0;
            NCurses.AttributeSet(CursesAttribute.NORMAL);
            NCurses.Nap(DefaultFramerate);
            NCurses.Erase();
            for (int i = 0; i < Buffer.Height; i++)
            {
                for (int j = 0; j < Buffer.Width; j++)
                {
                    NCurses.AttributeSet(NCurses.ColorPair(_colorMap[Buffer[offset++]]));
                    NCurses.MoveAddChar(i, j, ' ');
                }
            }
            NCurses.Refresh();
        }
    }
}
