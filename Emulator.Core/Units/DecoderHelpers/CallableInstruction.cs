using System;

using Emulator.Core.Interfaces.Instructions;

namespace Emulator.Core.Units.DecoderHelpers
{
    public delegate void CallableInstructionDelegate();

    public class CallableInstruction : ICPUInstruction
    {
        private CallableInstructionDelegate _func;

        public CallableInstruction Setup(CallableInstructionDelegate func)
        {
            _func = func;
            return this;
        }

        public void Execute()
        {
            _func();
        }
    }
}
