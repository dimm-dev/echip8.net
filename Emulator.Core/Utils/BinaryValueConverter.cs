using System;

namespace Emulator.Core.Utils
{
    public class BinaryValueConverter
    {
        private static Byte[][] NibblePresentation = new Byte[][]
        {
           new Byte[] { 0, 0, 0, 0 },
           new Byte[] { 0, 0, 0, 1 },
           new Byte[] { 0, 0, 1, 0 },
           new Byte[] { 0, 0, 1, 1 },
           new Byte[] { 0, 1, 0, 0 },
           new Byte[] { 0, 1, 0, 1 },
           new Byte[] { 0, 1, 1, 0 },
           new Byte[] { 0, 1, 1, 1 },
           new Byte[] { 1, 0, 0, 0 },
           new Byte[] { 1, 0, 0, 1 },
           new Byte[] { 1, 0, 1, 0 },
           new Byte[] { 1, 0, 1, 1 },
           new Byte[] { 1, 1, 0, 0 },
           new Byte[] { 1, 1, 0, 1 },
           new Byte[] { 1, 1, 1, 0 },
           new Byte[] { 1, 1, 1, 1 }
        };

        public static void GetByteRepresentation(Byte[] presentation, int offset, Byte value)
        {
            Byte[] high = NibblePresentation[value >> 4];
            Byte[] low = NibblePresentation[value & 0x0F];
            Array.Copy(high, 0, presentation, offset, high.Length);
            Array.Copy(low, 0, presentation, high.Length + offset, low.Length);
        }
    }
}
