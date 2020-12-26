using System;

using Emulator.Core.Interfaces.Instructions;

namespace Emulator.Core.Units.DecoderHelpers
{
    public delegate void AddressOperationDelegate(Int16 param);

    public class AddressOperationInstruction : ICPUInstruction
    {
        private AddressOperationDelegate _func;
        private Int16 _param;

        public AddressOperationInstruction Setup(AddressOperationDelegate func, Int16 param)
        {
            _func = func;
            _param = param;
            return this;
        }

        public void Execute() => _func(_param);
    }
}
