using System;

namespace Emulator.Core.Interfaces.Periphery
{
    public interface IDisplay
    {
        Byte Width { get; }
        Byte Height { get; }

        void Clear();
        bool DrawSpriteXOR(Byte[] sprite, int spriteBytesCount, Byte x, Byte y);

        /// <summary>
        /// First event parameter is start video buffer position.
        /// Second event parameter is length of changed buffer.
        /// </summary>
        event Action<int, int> DisplayUpdateEvent;
    }
}
