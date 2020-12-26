using System;
using Emulator.Core.Interfaces.Units;

namespace Emulator.Core.Units
{
    public class InstructionPointerRegister : IInstructionPointerRegister
    {
        public const Int16 DefaultStart = 0x0200;
        public const Int16 DefaultEnd = 0x0FFF;
        public const Int16 InstructionSize = 2;

        private Int16 _current;
        private Int16 _start;
        private Int16 _end;

        public InstructionPointerRegister(Int16 end = DefaultEnd, Int16 start = DefaultStart) => UpdateRange(start, end);

        public Int16 Value { get => _current; }

        public Int16 Start { get => _start; }
        public Int16 End { get => _end; }

        public void UpdateRange(Int16 start, Int16 end)
        {
            if (start >= end)
                throw new ArgumentException($"GeneralInstructionPointerRegister: initialization arguments are invalid {start} >= {end}");

            if ((start & 1) != 0 || (end & 1) == 0)
                throw new ArgumentException($"GeneralInstructionPointerRegister: memory range from {start} to {end} must start with an odd address and must be odd length");

            _current = _start = start;
            _end = end;
        }

        public Int16 Next()
        {
            if (_current < End - InstructionSize)
                _current += InstructionSize;
            else
                InstructionPointerOutOfRange((Int16)(_current + InstructionSize));

            return _current;
        }

        public Int16 Prev()
        {
            if (_current >= Start + InstructionSize)
                _current -= InstructionSize;
            else
                InstructionPointerOutOfRange((Int16)(_current - InstructionSize));

            return _current;
        }

        public void Jump(Int16 address)
        {
            if ((address >= Start) && (address < End) && ((End - address - 1) % InstructionSize == 0))
            {
                _current = address;
                OnJump(_current);
            }
            else
                InstructionPointerOutOfRange(address);
        }

        public event Action<Int16> OnJump = delegate {};
    
        public event Action<Int16> InstructionPointerOutOfRange = delegate {};
    }
}
