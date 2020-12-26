using System;

using Emulator.Core.Interfaces.Instructions;

namespace Emulator.Core.Tests.InstructionParserHelpers
{
    public enum ParsedInstructionCode
    {
        Unknown,
        ClearScreen,
        Return,
        Call,
        Jump,
        SkipIfRegValue,
        SkipIfNotRegValue,
        SkipIfRegReg,
        LoadRegValue,
        AddRegValue,
        SkipIfNotRegReg,
        LoadRegReg,
        OrRegReg,
        AndRegReg,
        XorRegReg,
        AddRegReg,
        SubRegReg,
        ShrReg,
        SubnRegReg,
        ShlReg,
        LoadAddrReg,
        JumpAddrReg,
        GenRandom,
        DrawSprite,
        SkipIfKey,
        SkipIfKeyNot,
        LoadRegisterDelayTimer,
        WaitKeyPressed,
        LoadDelayTimer,
        LoadSoundCounter,
        AddAddressRegReg,
        LoadSymbolSprite,
        LoadBCD,
        LoadRegistersValue,
        LoadValueRegisters
    };

    public class InstructionStub : ICPUInstruction
    {
        public ParsedInstructionCode Code { get; }
        public InstructionStub(ParsedInstructionCode code) => Code = code;
        public void Execute() {}
    }

    public class AddressOperationStub : InstructionStub, IComparable<AddressOperationStub>
    {
        public Int16 Address { get; }

        public AddressOperationStub(ParsedInstructionCode code, Int16 address) : base(code) => Address = address;

        public int CompareTo(AddressOperationStub other)
        {
            if (Code.CompareTo(other.Code) != 0)
                return Code.CompareTo(other.Code);

            return Address.CompareTo(other.Address);
        }
    }

    public class BinaryRegisterOperationStub : InstructionStub, IComparable<BinaryRegisterOperationStub>
    {
        public Byte R0 { get; }
        public Byte R1 { get; }

        public BinaryRegisterOperationStub(ParsedInstructionCode code, Byte r0, Byte r1) : base(code)
        {
            R0 = r0;
            R1 = r1;
        }

        public BinaryRegisterOperationStub(ParsedInstructionCode code, Byte r0) : this(code, r0, 0) {}

        public int CompareTo(BinaryRegisterOperationStub other)
        {
            if (Code.CompareTo(other.Code) != 0)
                return Code.CompareTo(other.Code);

            if ((R0 < other.R0) || ((R0 == other.R0) && (R1 < other.R1)))
                return -1;

            if (R0 == other.R0 && R1 == other.R1)
                return 0;

            return 1;
        }
    }

    public class CallableOperationStub : InstructionStub, IComparable<CallableOperationStub>
    {
        public CallableOperationStub(ParsedInstructionCode code) : base(code) {}

        public int CompareTo(CallableOperationStub other) => Code.CompareTo(other.Code);
    }
}
