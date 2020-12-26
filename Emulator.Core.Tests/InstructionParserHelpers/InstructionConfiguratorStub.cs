using System;

using Emulator.Core.Interfaces.Instructions;
using Emulator.Core.Interfaces.Units.DecoderHelpers;

namespace Emulator.Core.Tests.InstructionParserHelpers
{
    public class InstructionConfiguratorStub : IInstructionConfigurator
    {
        public ICPUInstruction ConfigureClearScreenInstruction() => new CallableOperationStub(ParsedInstructionCode.ClearScreen);
        public ICPUInstruction ConfigureReturnInstruction() => new CallableOperationStub(ParsedInstructionCode.Return);
        public ICPUInstruction ConfigureJumpInstruction(Int16 address) => new AddressOperationStub(ParsedInstructionCode.Jump, address);
        public ICPUInstruction ConfigureCallInstruction(Int16 address) => new AddressOperationStub(ParsedInstructionCode.Call, address);
        public ICPUInstruction ConfigureSkipIfRegisterValueInstruction(Byte registerNumber, Byte value) => new BinaryRegisterOperationStub(ParsedInstructionCode.SkipIfRegValue, registerNumber, value);
        public ICPUInstruction ConfigureSkipIfNotRegisterValueInstruction(Byte registerNumber, Byte value) => new BinaryRegisterOperationStub(ParsedInstructionCode.SkipIfNotRegValue, registerNumber, value);
        public ICPUInstruction ConfigureSkipIfRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => new BinaryRegisterOperationStub(ParsedInstructionCode.SkipIfRegReg, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureLoadRegisterValueInstruction(Byte startRegister, Byte value) => new BinaryRegisterOperationStub(ParsedInstructionCode.LoadRegValue, startRegister, value);
        public ICPUInstruction ConfigureAddRegisterValueInstruction(Byte startRegister, Byte value) => new BinaryRegisterOperationStub(ParsedInstructionCode.AddRegValue, startRegister, value);
        public ICPUInstruction ConfigureSkipIfNotRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => new BinaryRegisterOperationStub(ParsedInstructionCode.SkipIfNotRegReg, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureLoadRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => new BinaryRegisterOperationStub(ParsedInstructionCode.LoadRegReg, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureOrRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => new BinaryRegisterOperationStub(ParsedInstructionCode.OrRegReg, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureAndRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => new BinaryRegisterOperationStub(ParsedInstructionCode.AndRegReg, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureXorRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => new BinaryRegisterOperationStub(ParsedInstructionCode.XorRegReg, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureAddRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => new BinaryRegisterOperationStub(ParsedInstructionCode.AddRegReg, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureSubRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => new BinaryRegisterOperationStub(ParsedInstructionCode.SubRegReg, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureShrRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => new BinaryRegisterOperationStub(ParsedInstructionCode.ShrReg, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureSubnRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => new BinaryRegisterOperationStub(ParsedInstructionCode.SubnRegReg, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureShlRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => new BinaryRegisterOperationStub(ParsedInstructionCode.ShlReg, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureLoadAddressRegisterInstruction(Int16 value) => new AddressOperationStub(ParsedInstructionCode.LoadAddrReg, value);
        public ICPUInstruction ConfigureJumpAddressRegisterInstruction(Int16 value) => new AddressOperationStub(ParsedInstructionCode.JumpAddrReg, value);
        public ICPUInstruction ConfigureGenerateRandomNumberInstruction(Byte registerNumber0, Byte value) => new BinaryRegisterOperationStub(ParsedInstructionCode.GenRandom, registerNumber0, value);
        public ICPUInstruction ConfigureDrawSpriteInstruction(Byte registerNumber0, Byte registerNumber1) => new BinaryRegisterOperationStub(ParsedInstructionCode.DrawSprite, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureSkipIfKeyPressedInstruction(Byte keyCode) => new BinaryRegisterOperationStub(ParsedInstructionCode.SkipIfKey, keyCode);
        public ICPUInstruction ConfigureSkipIfKeyNotPressedInstruction(Byte keyCode) => new BinaryRegisterOperationStub(ParsedInstructionCode.SkipIfKeyNot, keyCode);
        public ICPUInstruction ConfigureLoadRegisterDelayTimerInstruction(Byte registerNumber) => new BinaryRegisterOperationStub(ParsedInstructionCode.LoadRegisterDelayTimer, registerNumber);
        public ICPUInstruction ConfigureWaitKeyPressedInstruction(Byte keyCode) => new BinaryRegisterOperationStub(ParsedInstructionCode.WaitKeyPressed, keyCode);
        public ICPUInstruction ConfigureLoadDelayTimerInstruction(Byte registerNumber) => new BinaryRegisterOperationStub(ParsedInstructionCode.LoadDelayTimer, registerNumber);
        public ICPUInstruction ConfigureLoadSoundCounterInstruction(Byte registerNumber) => new BinaryRegisterOperationStub(ParsedInstructionCode.LoadSoundCounter, registerNumber);
        public ICPUInstruction ConfigureAddAddressRegisterRegisterInstruction(Byte registerNumber) => new BinaryRegisterOperationStub(ParsedInstructionCode.AddAddressRegReg, registerNumber);
        public ICPUInstruction ConfigureLoadSymbolSpriteInstruction(Byte spriteNum) => new BinaryRegisterOperationStub(ParsedInstructionCode.LoadSymbolSprite, spriteNum);
        public ICPUInstruction ConfigureLoadRegistersBCDValueInstruction(Byte bcdNum) => new BinaryRegisterOperationStub(ParsedInstructionCode.LoadBCD, bcdNum);
        public ICPUInstruction ConfigureLoadRegistersValueInstruction(Byte registerNumber) => new BinaryRegisterOperationStub(ParsedInstructionCode.LoadRegistersValue, registerNumber);
        public ICPUInstruction ConfigureLoadValueRegistersInstruction(Byte registerNumber) => new BinaryRegisterOperationStub(ParsedInstructionCode.LoadValueRegisters, registerNumber);
    }
}
