using System;
using Emulator.Core.Interfaces.Units;

namespace Emulator.Core.Units
{
    public class MemoryManagementUnit : IMemoryManagementUnit
    {
        public const int DefaultMemorySize = 0x0FFF + 1;

        private readonly Byte[] _memory;

        private static void CheckMemorySize(int memorySize)
        {
            if ((memorySize <= 0) || (memorySize & 1) != 0)
                throw new ArgumentException($"MemoryUnit: given memory size of {memorySize} is invalid. Must be positive and odd", "memorySize");
        }

        public MemoryManagementUnit(Byte[] memory)
        {
            if (memory == null)
                throw new ArgumentException("MemoryUnit: memory buffer must be not null", "memory");

            CheckMemorySize(memory.Length);
            _memory = memory;
            Size = (UInt16)_memory.Length;
        }

        public MemoryManagementUnit(int memorySize = DefaultMemorySize)
        {
            CheckMemorySize(memorySize);
            _memory = new Byte[memorySize];
            Size = memorySize;
        }

        public int Size { get; }

        // TODO: checks
        public Byte this[int i]
        {
            get => _memory[i];
            set => _memory[i] = value;
        }
    }
}
