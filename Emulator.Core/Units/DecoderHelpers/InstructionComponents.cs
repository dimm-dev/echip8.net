using System;

namespace Emulator.Core.Units.DecoderHelpers
{
    public class InstructionNibble
    {
        private Byte _high;
        private Byte _low;

        public Byte High { get => _high; }
        public Byte Low { get => _low; }

        public InstructionNibble SetValue(Byte value)
        {
            _high = (Byte)(value >> 4);
            _low = (Byte)(value & 0x0F);

            return this;
        }

        public InstructionNibble SetValue(Int16 value)
        {
            _high = (Byte)(value >> 8);
            _low = (Byte)(value & 0xFF);

            return this;
        }
    }

    public class InstructionComponents
    {
        private Byte _code;
        private InstructionNibble _octets = new InstructionNibble();
        private InstructionNibble _lowNibbles = new InstructionNibble();
        private InstructionNibble _highNibbles = new InstructionNibble();

        public InstructionNibble Octets { get => _octets; }
        public InstructionNibble LowNibbles { get => _lowNibbles; }
        public InstructionNibble HighNibbles { get => _highNibbles; }

        public Byte Code { get => _code; }
        public Int16 Value { get => (Int16)((HighNibbles.Low << 8) | Octets.Low); }

        public InstructionComponents SetValue(Int16 code)
        {
            int leValue = (code >> 8) | (code << 8);
            Octets.SetValue((Int16)leValue);
            LowNibbles.SetValue((Byte)(code >> 8));
            HighNibbles.SetValue((Byte)(code & 0x00FF));
            _code = (Byte)((code & 0x00F0) >> 4);

            return this;
        }
    }
}
