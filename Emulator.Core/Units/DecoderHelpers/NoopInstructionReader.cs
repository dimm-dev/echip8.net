using System;
using System.Collections;
using Emulator.Core.Interfaces.Units;
using Emulator.Core.Interfaces.Units.DecoderHelpers;

namespace Emulator.Core.Units.DecoderHelpers
{
    public class NoopInstructionReader : IInstructionReader
    {
        private IInstructionPointerRegister IP { get; }

        public NoopInstructionReader(IInstructionPointerRegister ipr)
        {
            IP = ipr;
        }

        public bool MoveNext() => IP.Value < IP.End;
        public Int16 Current { get => 0x0000; }
        object IEnumerator.Current { get => 0x0000; }
        public void Reset() {}
        public void Dispose() {}
    }
}
