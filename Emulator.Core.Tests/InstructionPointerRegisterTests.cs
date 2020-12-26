using System;
using Xunit;

using Emulator.Core.Units;

namespace Emulator.Core.Tests
{
    public class InstructionPointerRegisterTests
    {
        private class InstructionPointerRangeOutOfRangeException : Exception
        {
        }

        InstructionPointerRegister MakeRegister(Int16 end = InstructionPointerRegister.DefaultEnd, Int16 start = InstructionPointerRegister.DefaultStart)
        {
            var reg = new InstructionPointerRegister(end, start);
            reg.InstructionPointerOutOfRange += (i) => throw new InstructionPointerRangeOutOfRangeException();
            return reg;
        }

        [Fact]
        public void InstructionPointerRegisterCreateTests()
        {
            Assert.Throws<ArgumentException>(() => MakeRegister(0, 1));
            Assert.Throws<ArgumentException>(() => MakeRegister(128, 0));
            Assert.Throws<ArgumentException>(() => MakeRegister(129, 5));
            InstructionPointerRegister reg = MakeRegister();
            Assert.True(reg.End == InstructionPointerRegister.DefaultEnd);
            Assert.True(reg.Start == InstructionPointerRegister.DefaultStart);
            Assert.True(reg.Value == reg.Start);
            Assert.True((reg.End - reg.Start + 1) % 2 == 0);
        }

        [Fact]
        public void InstructionPointerRegisterNextPrevTests()
        {
            InstructionPointerRegister reg = MakeRegister();
            Assert.True(reg.Value == reg.Start);
            Int16 addr = reg.Next();
            Assert.True(addr > reg.Start);
            Assert.True(reg.Next() > addr);
            addr = reg.Value;
            Assert.True(reg.Prev() < addr);
            Assert.True(reg.Prev() == reg.Start);
            Assert.True(reg.Value == reg.Start);
        }

        [Fact]
        public void InstructionPointerRegistersOverflowTests()
        {
            InstructionPointerRegister reg = MakeRegister(11, 6);
            reg.Next(); reg.Next();
            Assert.Throws<InstructionPointerRangeOutOfRangeException>(() => reg.Next());
            Assert.True(reg.Value < reg.End);
            reg.Prev(); reg.Prev();
            Assert.Throws<InstructionPointerRangeOutOfRangeException>(() => reg.Prev());
            Assert.True(reg.Value == reg.Start);
        }
    }
}
