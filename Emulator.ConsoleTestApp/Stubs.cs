using System;

using Emulator.Core.Interfaces.Periphery;

namespace Emulator.ConsoleTestApp
{
    public class KeyboardStub : IKeyboard
    {
        public bool IsKeyPressed(Byte index) => false;
        public void KeyPressWait(KeyPressedDelegate callback) {}

        public void KeyDown(Byte index) {}
        public void KeyUp(Byte index) {}

        public void Tick() {}
    }
}
