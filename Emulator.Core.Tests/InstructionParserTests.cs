using System;

using Xunit;

using Emulator.Core.Tests.InstructionParserHelpers;
using Emulator.Core.Units.DecoderHelpers;

namespace Emulator.Core.Tests
{
    public class InstructionParserTests
    {
        void TestInvalidInstructionCall(byte[] byteCode)
        {
            Int16 instructionCode = BitConverter.ToInt16(byteCode);
            InstructionConfiguratorStub configurator = new InstructionConfiguratorStub();
            InstructionParser parser = new InstructionParser(configurator);
            parser.UnknownInstructionFoundEvent += (Int16 code) => throw new InvalidInstructionException();
            Assert.Throws<InvalidInstructionException>(() => parser.Parse(instructionCode));
        }

        void TestParseInstruction<T>(byte[] byteCode, IComparable<T> expected) where T : class
        {
            Int16 instructionCode = BitConverter.ToInt16(byteCode);
            InstructionConfiguratorStub configurator = new InstructionConfiguratorStub();
            InstructionParser parser = new InstructionParser(configurator);
            var parsed = parser.Parse(instructionCode) as T;
            Assert.Equal(0, expected.CompareTo(parsed));
        }

        [Fact]
        void ClearScreenInstructionTests() => TestParseInstruction(new byte[] { 0x00, 0xE0 }, new CallableOperationStub(ParsedInstructionCode.ClearScreen));

        [Fact]
        void ReturnInstructionTests() => TestParseInstruction(new byte[] { 0x00, 0xEE }, new CallableOperationStub(ParsedInstructionCode.Return));

        [Theory]
        [InlineData(new byte[] { 0x00, 0x00 })]
        [InlineData(new byte[] { 0x00, 0xE1 })]
        [InlineData(new byte[] { 0x00, 0x10 })]
        [InlineData(new byte[] { 0x00, 0xAE })]
        void TestInvalidInstruction0(byte[] byteCode) => TestInvalidInstructionCall(byteCode);

        [Theory]
        [InlineData(new byte[] { 0x10, 0x00 }, 0x0000)]
        [InlineData(new byte[] { 0x10, 0x01 }, 0x0001)]
        [InlineData(new byte[] { 0x10, 0x10 }, 0x0010)]
        [InlineData(new byte[] { 0x11, 0x11 }, 0x0111)]
        [InlineData(new byte[] { 0x1F, 0xFF }, 0x0FFF)]
        void TestJumpInstruction(byte[] byteCode, Int16 expected) => TestParseInstruction(byteCode, new AddressOperationStub(ParsedInstructionCode.Jump, expected));

        [Theory]
        [InlineData(new byte[] { 0x20, 0x00 }, 0x0000)]
        [InlineData(new byte[] { 0x20, 0x01 }, 0x0001)]
        [InlineData(new byte[] { 0x20, 0x10 }, 0x0010)]
        [InlineData(new byte[] { 0x21, 0x11 }, 0x0111)]
        [InlineData(new byte[] { 0x2F, 0xFF }, 0x0FFF)]
        void TestCallInstruction(byte[] byteCode, Int16 expected) => TestParseInstruction(byteCode, new AddressOperationStub(ParsedInstructionCode.Call, expected));

        [Theory]
        [InlineData(new byte[] { 0x30, 0x00 }, 0x00, 0x00)]
        [InlineData(new byte[] { 0x31, 0x01 }, 0x01, 0x01)]
        [InlineData(new byte[] { 0x32, 0x10 }, 0x02, 0x10)]
        [InlineData(new byte[] { 0x32, 0x11 }, 0x02, 0x11)]
        [InlineData(new byte[] { 0x3F, 0xFF }, 0x0F, 0xFF)]
        void TestSkifIfRegisterEqualInstruction(byte[] byteCode, Byte regNum, Byte value) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.SkipIfRegValue, regNum, value));

        [Theory]
        [InlineData(new byte[] { 0x40, 0x00 }, 0x00, 0x00)]
        [InlineData(new byte[] { 0x41, 0x01 }, 0x01, 0x01)]
        [InlineData(new byte[] { 0x42, 0x10 }, 0x02, 0x10)]
        [InlineData(new byte[] { 0x42, 0x11 }, 0x02, 0x11)]
        [InlineData(new byte[] { 0x4F, 0xFF }, 0x0F, 0xFF)]
        void TestSkifIfRegisterNotEqualInstruction(byte[] byteCode, Byte regNum, Byte value) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.SkipIfNotRegValue, regNum, value));

        [Theory]
        [InlineData(new byte[] { 0x50, 0x00 }, 0x00, 0x00)]
        [InlineData(new byte[] { 0x51, 0x01 }, 0x01, 0x00)]
        [InlineData(new byte[] { 0x52, 0x10 }, 0x02, 0x01)]
        [InlineData(new byte[] { 0x52, 0x11 }, 0x02, 0x01)]
        [InlineData(new byte[] { 0x5F, 0xFF }, 0x0F, 0x0F)]
        void TestSkifIfRegistersEqualInstruction(byte[] byteCode, Byte regNum0, Byte regNum1) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.SkipIfRegReg, regNum0, regNum1));

        [Theory]
        [InlineData(new byte[] { 0x60, 0x00 }, 0x00, 0x00)]
        [InlineData(new byte[] { 0x61, 0x01 }, 0x01, 0x01)]
        [InlineData(new byte[] { 0x62, 0x10 }, 0x02, 0x10)]
        [InlineData(new byte[] { 0x62, 0x11 }, 0x02, 0x11)]
        [InlineData(new byte[] { 0x6F, 0xFF }, 0x0F, 0xFF)]
        void TestLoadRegisterValueInstruction(byte[] byteCode, Byte regNum0, Byte value) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.LoadRegValue, regNum0, value));

        [Theory]
        [InlineData(new byte[] { 0x70, 0x00 }, 0x00, 0x00)]
        [InlineData(new byte[] { 0x71, 0x01 }, 0x01, 0x01)]
        [InlineData(new byte[] { 0x72, 0x10 }, 0x02, 0x10)]
        [InlineData(new byte[] { 0x72, 0x11 }, 0x02, 0x11)]
        [InlineData(new byte[] { 0x7F, 0xFF }, 0x0F, 0xFF)]
        void TestAddRegisterValueInstruction(byte[] byteCode, Byte regNum0, Byte value) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.AddRegValue, regNum0, value));

        [Theory]
        [InlineData(new byte[] { 0x80, 0x00 }, 0x00, 0x00)]
        [InlineData(new byte[] { 0x82, 0x10 }, 0x02, 0x01)]
        [InlineData(new byte[] { 0x8F, 0xF0 }, 0x0F, 0x0F)]
        void TestLoadRegisterRegisterInstruction(byte[] byteCode, Byte regNum0, Byte regNum1) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.LoadRegReg, regNum0, regNum1));

        [Theory]
        [InlineData(new byte[] { 0x80, 0x01 }, 0x00, 0x00)]
        [InlineData(new byte[] { 0x82, 0x11 }, 0x02, 0x01)]
        [InlineData(new byte[] { 0x8F, 0xF1 }, 0x0F, 0x0F)]
        void TestOrRegisterRegisterInstruction(byte[] byteCode, Byte regNum0, Byte regNum1) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.OrRegReg, regNum0, regNum1));

        [Theory]
        [InlineData(new byte[] { 0x80, 0x02 }, 0x00, 0x00)]
        [InlineData(new byte[] { 0x82, 0x12 }, 0x02, 0x01)]
        [InlineData(new byte[] { 0x8F, 0xF2 }, 0x0F, 0x0F)]
        void TestAndRegisterRegisterInstruction(byte[] byteCode, Byte regNum0, Byte regNum1) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.AndRegReg, regNum0, regNum1));

        [Theory]
        [InlineData(new byte[] { 0x80, 0x03 }, 0x00, 0x00)]
        [InlineData(new byte[] { 0x82, 0x13 }, 0x02, 0x01)]
        [InlineData(new byte[] { 0x8F, 0xF3 }, 0x0F, 0x0F)]
        void TestXorRegisterRegisterInstruction(byte[] byteCode, Byte regNum0, Byte regNum1) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.XorRegReg, regNum0, regNum1));

        [Theory]
        [InlineData(new byte[] { 0x80, 0x04 }, 0x00, 0x00)]
        [InlineData(new byte[] { 0x82, 0x14 }, 0x02, 0x01)]
        [InlineData(new byte[] { 0x8F, 0xF4 }, 0x0F, 0x0F)]
        void TestAddRegisterRegisterInstruction(byte[] byteCode, Byte regNum0, Byte regNum1) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.AddRegReg, regNum0, regNum1));

        [Theory]
        [InlineData(new byte[] { 0x80, 0x05 }, 0x00, 0x00)]
        [InlineData(new byte[] { 0x82, 0x15 }, 0x02, 0x01)]
        [InlineData(new byte[] { 0x8F, 0xF5 }, 0x0F, 0x0F)]
        void TestSubRegisterRegisterInstruction(byte[] byteCode, Byte regNum0, Byte regNum1) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.SubRegReg, regNum0, regNum1));

        [Theory]
        [InlineData(new byte[] { 0x80, 0x06 }, 0x00, 0x00)]
        [InlineData(new byte[] { 0x82, 0x16 }, 0x02, 0x01)]
        [InlineData(new byte[] { 0x8F, 0xF6 }, 0x0F, 0x0F)]
        void TestShrRegisterRegisterInstruction(byte[] byteCode, Byte regNum0, Byte regNum1) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.ShrReg, regNum0, regNum1));

        [Theory]
        [InlineData(new byte[] { 0x80, 0x07 }, 0x00, 0x00)]
        [InlineData(new byte[] { 0x82, 0x17 }, 0x02, 0x01)]
        [InlineData(new byte[] { 0x8F, 0xF7 }, 0x0F, 0x0F)]
        void TestSubnRegisterRegisterInstruction(byte[] byteCode, Byte regNum0, Byte regNum1) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.SubnRegReg, regNum0, regNum1));

        [Theory]
        [InlineData(new byte[] { 0x80, 0x0E }, 0x00, 0x00)]
        [InlineData(new byte[] { 0x82, 0x1E }, 0x02, 0x01)]
        [InlineData(new byte[] { 0x8F, 0xFE }, 0x0F, 0x0F)]
        void TestShlRegisterRegisterInstruction(byte[] byteCode, Byte regNum0, Byte regNum1) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.ShlReg, regNum0, regNum1));

        [Theory]
        [InlineData(new byte[] { 0x80, 0x08 })]
        [InlineData(new byte[] { 0x80, 0x09 })]
        [InlineData(new byte[] { 0x80, 0x0A })]
        [InlineData(new byte[] { 0x80, 0x0F })]
        void TestInvalidInstruction8(byte[] byteCode) => TestInvalidInstructionCall(byteCode);

        [Theory]
        [InlineData(new byte[] { 0x90, 0x00 }, 0x00, 0x00)]
        [InlineData(new byte[] { 0x91, 0x01 }, 0x01, 0x00)]
        [InlineData(new byte[] { 0x92, 0x10 }, 0x02, 0x01)]
        [InlineData(new byte[] { 0x92, 0x11 }, 0x02, 0x01)]
        [InlineData(new byte[] { 0x9F, 0xFF }, 0x0F, 0x0F)]
        void TestSkipIfRegistersNotEqualInstruction(byte[] byteCode, Byte regNum0, Byte regNum1) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.SkipIfNotRegReg, regNum0, regNum1));

        [Theory]
        [InlineData(new byte[] { 0xA0, 0x00 }, 0x0000)]
        [InlineData(new byte[] { 0xA0, 0x01 }, 0x0001)]
        [InlineData(new byte[] { 0xA0, 0x10 }, 0x0010)]
        [InlineData(new byte[] { 0xA1, 0x00 }, 0x0100)]
        [InlineData(new byte[] { 0xA1, 0x11 }, 0x0111)]
        [InlineData(new byte[] { 0xAF, 0xFF }, 0x0FFF)]
        void TestLoadAddressRegisterInstruction(byte[] byteCode, Int16 expected) => TestParseInstruction(byteCode, new AddressOperationStub(ParsedInstructionCode.LoadAddrReg, expected));

        [Theory]
        [InlineData(new byte[] { 0xB0, 0x00 }, 0x0000)]
        [InlineData(new byte[] { 0xB0, 0x01 }, 0x0001)]
        [InlineData(new byte[] { 0xB0, 0x10 }, 0x0010)]
        [InlineData(new byte[] { 0xB1, 0x00 }, 0x0100)]
        [InlineData(new byte[] { 0xB1, 0x11 }, 0x0111)]
        [InlineData(new byte[] { 0xBF, 0xFF }, 0x0FFF)]
        void TestJumpAddressRegisterInstruction(byte[] byteCode, Int16 expected) => TestParseInstruction(byteCode, new AddressOperationStub(ParsedInstructionCode.JumpAddrReg, expected));

        [Theory]
        [InlineData(new byte[] { 0xC0, 0x00 }, 0x00, 0x00)]
        [InlineData(new byte[] { 0xC1, 0x01 }, 0x01, 0x01)]
        [InlineData(new byte[] { 0xC2, 0x10 }, 0x02, 0x10)]
        [InlineData(new byte[] { 0xC2, 0x11 }, 0x02, 0x11)]
        [InlineData(new byte[] { 0xCF, 0xFF }, 0x0F, 0xFF)]
        void TestGenerateRandomInstruction(byte[] byteCode, Byte regNum0, Byte regNum1) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.GenRandom, regNum0, regNum1));

        [Theory]
        [InlineData(new byte[] { 0xD0, 0x00 }, 0x00, 0x00)]
        [InlineData(new byte[] { 0xD1, 0x01 }, 0x01, 0x01)]
        [InlineData(new byte[] { 0xD2, 0x10 }, 0x02, 0x10)]
        [InlineData(new byte[] { 0xD2, 0x11 }, 0x02, 0x11)]
        [InlineData(new byte[] { 0xDF, 0xFF }, 0x0F, 0xFF)]
        void TestDrawSpriteInstruction(byte[] byteCode, Byte regNum0, Byte regNum1) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.DrawSprite, regNum0, regNum1));

        [Theory]
        [InlineData(new byte[] { 0xE0, 0x9E }, 0x00)]
        [InlineData(new byte[] { 0xE1, 0x9E }, 0x01)]
        [InlineData(new byte[] { 0xEF, 0x9E }, 0x0F)]
        void TestSkipIfKeyPressedInstruction(byte[] byteCode, Byte keyNum) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.SkipIfKey, keyNum));

        [Theory]
        [InlineData(new byte[] { 0xE0, 0xA1 }, 0x00)]
        [InlineData(new byte[] { 0xE1, 0xA1 }, 0x01)]
        [InlineData(new byte[] { 0xEF, 0xA1 }, 0x0F)]
        void TestSkipIfKeyNotPressedInstruction(byte[] byteCode, Byte keyNum) => TestParseInstruction(byteCode, new BinaryRegisterOperationStub(ParsedInstructionCode.SkipIfKeyNot, keyNum));

        [Theory]
        [InlineData(new byte[] { 0xE0, 0x01 })]
        [InlineData(new byte[] { 0xE0, 0x02 })]
        [InlineData(new byte[] { 0xE0, 0x9F })]
        [InlineData(new byte[] { 0xE0, 0xA2 })]
        void TestInvalidInstructionE(byte[] byteCode) => TestInvalidInstructionCall(byteCode);

        void TestInstructionFParse(Byte subCode, ParsedInstructionCode exepected)
        {
            Byte[][] data = new Byte[][]
            {
                new Byte[] { 0xF0, subCode },
                new Byte[] { 0xF1, subCode },
                new Byte[] { 0xFF, subCode }
            };
            Byte[] expectedReg = new Byte[]
            {
                0x00, 0x01, 0x0F
            };
            for (int i = 0; i < data.Length; i++)
                TestParseInstruction(data[i], new BinaryRegisterOperationStub(exepected, expectedReg[i]));
        }

        [Fact]
        void TestLoadDelayTimerInstruction() => TestInstructionFParse(0x07, ParsedInstructionCode.LoadRegisterDelayTimer);

        [Fact]
        void TestWaitKeyPressedInstruction() => TestInstructionFParse(0x0A, ParsedInstructionCode.WaitKeyPressed);

        [Fact]
        void TestSetDelayTimerInstruction() => TestInstructionFParse(0x15, ParsedInstructionCode.LoadDelayTimer);

        [Fact]
        void TestSetSoundTimerInstruction() => TestInstructionFParse(0x18, ParsedInstructionCode.LoadSoundCounter);

        [Fact]
        void TestAddAddressInstruction() => TestInstructionFParse(0x1E, ParsedInstructionCode.AddAddressRegReg);

        [Fact]
        void TestLoadSymbolSpriteInstructio2() => TestInstructionFParse(0x29, ParsedInstructionCode.LoadSymbolSprite);

        [Fact]
        void TestLoadBCDInstruction() => TestInstructionFParse(0x33, ParsedInstructionCode.LoadBCD);

        [Fact]
        void TestValueRegistersInstruction() => TestInstructionFParse(0x55, ParsedInstructionCode.LoadRegistersValue);

        [Fact]
        void TestRegisterValuesInstruction() => TestInstructionFParse(0x65, ParsedInstructionCode.LoadValueRegisters);
    }
}
