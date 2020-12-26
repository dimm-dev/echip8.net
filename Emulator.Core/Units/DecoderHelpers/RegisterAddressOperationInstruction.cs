using System;

using Emulator.Core.Interfaces.Instructions;

namespace Emulator.Core.Units.DecoderHelpers
{
    public delegate void RegisterAddressOperationDelegate(Byte registerNum, Int16 address);

    public class RegisterAddressOperationInstruction : ICPUInstruction
    {
        private RegisterAddressOperationDelegate _func;
        Byte _registerNumber;
        Int16 _address;

        public RegisterAddressOperationInstruction Setup(RegisterAddressOperationDelegate func, Byte registerNum, Int16 address)
        {
            _func = func;
            _registerNumber = registerNum;
            _address = address;
            return this;
        }

        public void Execute() => _func(_registerNumber, _address);

    }
}
