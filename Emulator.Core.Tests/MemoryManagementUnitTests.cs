using System;
using System.Linq;
using Xunit;

using Emulator.Core.Units;

namespace Emulator.Core.Tests
{
    public class MemoryManagementUnitTests
    {
        private byte[] MakeMemoryDumpPattern0() => Enumerable.Range(0, MemoryManagementUnit.DefaultMemorySize).Select(i => (Byte)(i + 1)).ToArray();

        [Fact]
        public void MemorySizeAndAlignmentTests()
        {
            Assert.Throws<ArgumentException>(() => new MemoryManagementUnit(0));
            Assert.Throws<ArgumentException>(() => new MemoryManagementUnit(MemoryManagementUnit.DefaultMemorySize + 1));
            Assert.Throws<ArgumentException>(() => new MemoryManagementUnit(5));

            Assert.Throws<ArgumentException>(() => new MemoryManagementUnit(null));
            Assert.Throws<ArgumentException>(() => new MemoryManagementUnit(new Byte[0]));
            Assert.Throws<ArgumentException>(() => new MemoryManagementUnit(new Byte[MemoryManagementUnit.DefaultMemorySize + 1]));
            Assert.Throws<ArgumentException>(() => new MemoryManagementUnit(new Byte[5]));
        }

        [Fact]
        public void MemoryByteReadTests()
        {
            Byte[] dump = MakeMemoryDumpPattern0();
            Byte[] expected = dump.Select(i => (Byte)(i + 1)).ToArray();
            MemoryManagementUnit memory = new MemoryManagementUnit(dump);
            Assert.True(Enumerable.Range(0, memory.Size).All(i => memory[i] == dump[i]));
            Assert.True(Enumerable.Range(0, memory.Size).Select(i => memory[i] = (Byte)(memory[i] + 1)).All(i => memory[i] == dump[i]));
            Assert.True(Enumerable.Range(0, memory.Size).All(i => memory[i] == expected[i]));
        }

        [Fact]
        public void MemoryReadOutOfRange()
        {
            Byte[] dump = MakeMemoryDumpPattern0();
            MemoryManagementUnit memory = new MemoryManagementUnit(dump);

            Assert.Throws<IndexOutOfRangeException>(() => memory[dump.GetLength(0)]);
        }
    }
}
