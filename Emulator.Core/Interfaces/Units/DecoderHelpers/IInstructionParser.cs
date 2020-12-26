using System;

using Emulator.Core.Interfaces.Instructions;

namespace Emulator.Core.Interfaces.Units.DecoderHelpers
{
    public interface IInstructionParser
    {
        ICPUInstruction Parse(Int16 instructionCode);
        event Action<Int16> UnknownInstructionFoundEvent;
    }
}
