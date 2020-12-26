using System;

namespace Emulator.Core.Interfaces.Periphery
{
    public delegate void KeyPressedDelegate(Byte index);

    public interface IKeyboardDataSource
    {
        bool IsKeyPressed(Byte index);
        void KeyPressWait(KeyPressedDelegate callback);
    }

    public interface IKeyboardEventSink
    {
        void KeyDown(Byte index);
        void KeyUp(Byte index);
    }

    public interface IKeyboard : IKeyboardDataSource, IKeyboardEventSink
    {
        void Tick();
    }
}
