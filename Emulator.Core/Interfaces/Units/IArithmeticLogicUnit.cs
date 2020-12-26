using System;

namespace Emulator.Core.Interfaces.Units
{
    public interface IArithmeticLogicUnit
    {
        IAddressRegister AddressRegister { get; }
        IGeneralPurposeRegisters Registers { get; }
        IInstructionPointerRegister IP { get; }
        IReturnStack ReturnStack { get; }

        void Reset();

        void Return();
        void Jump(Int16 address);
        void Call(Int16 address);
        void Skip();
        void SetOverflowFlag(bool value);
    }
}
