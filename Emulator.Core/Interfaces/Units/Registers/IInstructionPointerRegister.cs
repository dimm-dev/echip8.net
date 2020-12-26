using System;

namespace Emulator.Core.Interfaces.Units
{
    public interface IInstructionPointerRegister
    {
        Int16 Value { get; }

        Int16 Start { get; }
        Int16 End { get; }

        void UpdateRange(Int16 start, Int16 end);

        Int16 Next();
        Int16 Prev();

        void Jump(Int16 address);

        event Action<Int16> OnJump;
        event Action<Int16> InstructionPointerOutOfRange;
    }
}
