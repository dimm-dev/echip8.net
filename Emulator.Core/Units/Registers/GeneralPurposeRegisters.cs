using System;
using Emulator.Core.Interfaces.Units;

namespace Emulator.Core.Units
{
    public class GeneralPurposeRegisters : IGeneralPurposeRegisters
    {
        public const int DefaultRegistersCount = 16;

        private Byte[] _registers;

        public GeneralPurposeRegisters(int registersCount = DefaultRegistersCount)
        {
            // TODO: check
            // count >= 16
            if (registersCount <= 0)
                throw new ArgumentException($"GeneralPurposeRegisters: registers count must be positive. Current value is {registersCount}", "registersCount");

            _registers = new Byte[registersCount];
        }

        public int Count { get => _registers.GetLength(0); }

        // TODO: checks
        public Byte this[int num] { get => _registers[num]; set => _registers[num] = value; }
    }
}
