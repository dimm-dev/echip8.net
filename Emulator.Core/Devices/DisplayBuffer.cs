using System;

using Emulator.Core.Interfaces.Periphery;

namespace Emulator.Core
{
    public class DisplayBuffer : IDisplay
    {
        private const int SpriteWidth = 8;
        public Byte Width { get; }
        public Byte Height { get; }

        public Int32 ContentVersion { get => _contentVersion; }

        private Byte[] _buffer;
        private Int32 _contentVersion;

        public DisplayBuffer(Byte width, Byte height)
        {
            // TODO: check args
            Width = width;
            Height = height;
            _buffer = new byte[width * (height + 1)];
            _contentVersion = 0;
        }

        public void Clear()
        {
            for (int i = 0; i < _buffer.Length; i++)
                _buffer[i] = 0;
        }

        public bool DrawSpriteXOR(Byte[] sprite, int spriteBytesCount, Byte x, Byte y)
        {
            // TODO: check spriteBytesCount % 8 == 0 ?
            int bufferOffset = y * Width + x;
            int xorred = UpdateBuffer(sprite, spriteBytesCount, bufferOffset);
            if (xorred > 0)
            {
                _contentVersion++;
                DisplayUpdateEvent?.Invoke(bufferOffset, spriteBytesCount);
            }
            return xorred > 0;
        }

        private int UpdateBuffer(Byte[] sprite, int spriteBytesCount, int bufferOffset)
        {
            int xorred = 0;
            int spriteOffset = 0;
            for (; spriteBytesCount > 0; spriteBytesCount -= 8)
            {
                for (int j = 0; j < SpriteWidth; j++)
                {
                    xorred += Convert.ToInt32((sprite[spriteOffset + j] + _buffer[bufferOffset + j]) == 0);
                    _buffer[bufferOffset + j] ^= sprite[spriteOffset + j];
                }
                spriteOffset += SpriteWidth;
                bufferOffset += Width;
            }
            return xorred;
        }

        public Byte this[int index] => _buffer[index];

        public event Action<int, int> DisplayUpdateEvent;
    }
}
