using System;

using Emulator.Core.Interfaces.Periphery;

namespace Emulator.Core
{
    public class DisplayBuffer : IDisplay
    {
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
            int spriteOffset = 0;
            int xorred = 0;
            for (; spriteBytesCount > 0; spriteBytesCount -= 8, y++)
            {
                for (int j = 0; j < 8; j++)
                {
                    xorred += Convert.ToInt32((sprite[spriteOffset + j] + _buffer[bufferOffset + j]) == 2);
                    _buffer[bufferOffset + j] ^= sprite[spriteOffset + j];
                }
                spriteOffset += 8;
                bufferOffset += Width;
            }
            _contentVersion++; 
            return xorred > 0;
        }

        public Byte this[int index] => _buffer[index];
    }
}
