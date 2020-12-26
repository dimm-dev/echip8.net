using System;

using Emulator.Core.Interfaces.Instructions;

namespace Emulator.Core.Units.DecoderHelpers
{
    public delegate void BinaryOperationDelegate(Byte r0, Byte r1);
    public delegate void UpdateRegisterDelegate(int r, Byte val);

    public class BinaryRegistersOperationInstruction : ICPUInstruction
    {
        private BinaryOperationDelegate _func;
        private Byte _r0;
        private Byte _r1;

        public BinaryRegistersOperationInstruction Setup(BinaryOperationDelegate func, Byte r0, Byte r1)
        {
            _func = func;
            _r0 = r0;
            _r1 = r1;
            return this;
        }

        public void Execute() => _func(_r0, _r1);
    }
}
