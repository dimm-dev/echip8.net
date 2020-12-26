using System;

using Xunit;

using Emulator.Core.Units.DecoderHelpers;

namespace Emulator.Core.Tests
{
    public class BitManipulationTests
    {
        [Theory]
        [InlineData(0x0000, 0x00, 0x00)]
        [InlineData(0x0100, 0x01, 0x00)]
        [InlineData(0xFF00, 0xFF, 0x00)]
        [InlineData(0x00FF, 0x00, 0xFF)]
        [InlineData(0xF00F, 0xF0, 0x0F)]
        [InlineData(0x7A7B, 0x7A, 0x7B)]
        void InstructionNibbleInt16Test(UInt16 val, Byte high, Byte low)
        {
            var nibble = new InstructionNibble().SetValue((Int16)val);
            Assert.Equal(high, nibble.High);
            Assert.Equal(low, nibble.Low);
        }

        [Theory]
        [InlineData(0x00, 0x00, 0x00)]
        [InlineData(0x01, 0x00, 0x01)]
        [InlineData(0xF0, 0x0F, 0x00)]
        [InlineData(0x0F, 0x00, 0x0F)]
        [InlineData(0xFF, 0x0F, 0x0F)]
        void InstructionNibbleInt8Test(Byte val, Byte high, Byte low)
        {
            var nibble = new InstructionNibble().SetValue(val);
            Assert.Equal(high, nibble.High);
            Assert.Equal(low, nibble.Low);
        }

        [Theory]
        [InlineData(0x1EA2, 0x0A, 0x02, 0x1E)]
        [InlineData(0x01C2, 0x0C, 0x02, 0x01)]
        void InstructionComponentsTest(UInt16 val, Byte code, Byte nibble, Byte octet)
        {
            var instruction = new InstructionComponents().SetValue((Int16)val);
            Assert.Equal(code, instruction.Code);
            Assert.Equal(nibble, instruction.HighNibbles.Low);
            Assert.Equal(octet, instruction.Octets.Low);
            Assert.Equal(octet & 0x0F, instruction.LowNibbles.Low);
            Assert.Equal(octet >> 4, instruction.LowNibbles.High);
        }
    }
}
