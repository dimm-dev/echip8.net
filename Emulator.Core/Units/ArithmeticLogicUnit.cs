using System;
using Emulator.Core.Interfaces.Units;

namespace Emulator.Core.Units
{
    public class ArithmeticLogicUnit : IArithmeticLogicUnit
    {
        public IAddressRegister AddressRegister { get; }
        public IGeneralPurposeRegisters Registers { get; }
        public IInstructionPointerRegister IP { get; }
        public IReturnStack ReturnStack { get; }

        public ArithmeticLogicUnit(IAddressRegister adr, IGeneralPurposeRegisters gpr, IInstructionPointerRegister ip, IReturnStack stack)
        {
            AddressRegister = adr;
            Registers = gpr;
            IP = ip;
            ReturnStack = stack;
        }

        public void Reset()
        {
            AddressRegister.Value = 0;
            IP.Jump(IP.Start);
            ReturnStack.Reset();
        }

        public void Return()
        {
            // TODO: check pos.HasValue ?
            Int16 pos = ReturnStack.Pop();
            IP.Jump(pos);
        }

        public void Jump(Int16 address) => IP.Jump(address);

        public void Call(Int16 address)
        {
            Int16 ipr = (Int16)IP.Value;
            ReturnStack.Push(ipr);
            Jump(address);
        }

        public void Skip() => IP.Next();

        public void SetOverflowFlag(bool value) => Registers[Registers.Count - 1] = Convert.ToByte(value);
    }
}
