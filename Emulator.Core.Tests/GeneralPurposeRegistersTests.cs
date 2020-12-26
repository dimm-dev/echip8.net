using System;
using System.Linq;
using Xunit;

using Emulator.Core.Units;

namespace Emulator.Core.Tests
{
    public class GeneralPurposeRegistersTests
    {
        GeneralPurposeRegisters MakeRegisters(int count = GeneralPurposeRegisters.DefaultRegistersCount)
        {
            var gpr = new GeneralPurposeRegisters(count);
            return gpr;
        }

        [Fact]
        public void GeneralPurposeRegistersCreateTests()
        {
            Assert.Throws<ArgumentException>(() => MakeRegisters(-1));
            Assert.Throws<ArgumentException>(() => MakeRegisters(0));
            var r = MakeRegisters(1);
            Assert.True(r.Count == 1);
        }

        [Fact]
        public void AddressRegisterOverflowTests()
        {
            GeneralPurposeRegisters gpr = MakeRegisters();
            Enumerable.Range(0, gpr.Count).Any((i) => { gpr[i] = (Byte)(i + 1); return false; });
            Assert.True(gpr.Count == GeneralPurposeRegisters.DefaultRegistersCount);
            Assert.True(Enumerable.Range(0, gpr.Count).All((i) => gpr[i] == (UInt16)(i + 1)));
        }
    }
}
