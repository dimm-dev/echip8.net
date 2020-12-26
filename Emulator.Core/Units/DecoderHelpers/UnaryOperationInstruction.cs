using System;

using Emulator.Core.Interfaces.Instructions;

namespace Emulator.Core.Units.DecoderHelpers
{
    public delegate void UnaryOperationDelegate(Byte param);

    public class UnaryOperationInstruction : ICPUInstruction
    {
        private UnaryOperationDelegate _func;
        private Byte _param;

        public UnaryOperationInstruction Setup(UnaryOperationDelegate func, Byte param)
        {
            _func = func;
            _param = param;
            return this;
        }

        public void Execute() => _func(_param);
    }
}
