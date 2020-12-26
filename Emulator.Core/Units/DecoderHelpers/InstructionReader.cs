using System;
using System.Collections;
using Emulator.Core.Interfaces.Units;
using Emulator.Core.Interfaces.Units.DecoderHelpers;

namespace Emulator.Core.Units.DecoderHelpers
{
    public class InstructionReader : IInstructionReader
    {
        private delegate bool MoveNextInstructionDelegate();
        private IMemoryManagementUnit MMU { get; }
        private IInstructionPointerRegister IP { get; }
        private MoveNextInstructionDelegate Move { get; set; }

        public InstructionReader(IMemoryManagementUnit mmu, IInstructionPointerRegister ipr)
        {
            MMU = mmu;
            IP = ipr;
            IP.OnJump += OnJump;
            Reset();
        }

        public bool MoveNext() => Move();
        public Int16 Current { get => ReadInstruction(); }
        object IEnumerator.Current { get => ReadInstruction(); }
        public void Reset() => Move = NoopMove;

        public void Dispose() {}

        private bool NoopMove()
        {
            Move = MoveNextInstruction;
            return IP.Value < IP.End;
        }

        private bool MoveNextInstruction()
        {
            // TODO: without if?
            if (IP.Value < IP.End)
            {
                IP.Next();
                return true;
            }
            return false;
        }

        private void OnJump(Int16 address) => Move = NoopMove;

        Int16 ReadInstruction() => (Int16)((MMU[IP.Value + 1] << 8) | MMU[IP.Value]);
    }
}
