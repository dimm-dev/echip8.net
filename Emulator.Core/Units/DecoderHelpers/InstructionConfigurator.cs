using System;

using Emulator.Core.Interfaces.Instructions;
using Emulator.Core.Interfaces.Units;
using Emulator.Core.Interfaces.Units.DecoderHelpers;

namespace Emulator.Core.Units.DecoderHelpers
{
    public class InstructionConfigurator : IInstructionConfigurator
    {
        private ISystemBridge _bridge;

        private AddressOperationInstruction _addressOperation;
        private BinaryRegistersOperationInstruction _binaryRegistersOperation;
        private CallableInstruction _callableOperation;
        private RegisterAddressOperationInstruction _registerAddressOperation;
        private UnaryOperationInstruction _unrayOperation;

        public InstructionConfigurator(ISystemBridge bridge)
        {
            _bridge = bridge;
            _addressOperation = new AddressOperationInstruction();
            _binaryRegistersOperation = new BinaryRegistersOperationInstruction();
            _callableOperation = new CallableInstruction();
            _registerAddressOperation = new RegisterAddressOperationInstruction();
            _unrayOperation = new UnaryOperationInstruction();
        }

        public ICPUInstruction ConfigureClearScreenInstruction() => _callableOperation.Setup(_bridge.ClearScreen);
        public ICPUInstruction ConfigureReturnInstruction() => _callableOperation.Setup(_bridge.Return);
        public ICPUInstruction ConfigureJumpInstruction(Int16 address) => _addressOperation.Setup(_bridge.Jump, address);
        public ICPUInstruction ConfigureCallInstruction(Int16 address) => _addressOperation.Setup(_bridge.Call, address);
        public ICPUInstruction ConfigureSkipIfRegisterValueInstruction(Byte registerNumber, Byte value) => _binaryRegistersOperation.Setup(_bridge.SkipIfRegisterValueEqual, registerNumber, value);
        public ICPUInstruction ConfigureSkipIfNotRegisterValueInstruction(Byte registerNumber, Byte value) => _binaryRegistersOperation.Setup(_bridge.SkipIfNotRegisterValueEqual, registerNumber, value);
        public ICPUInstruction ConfigureSkipIfRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => _binaryRegistersOperation.Setup(_bridge.SkipIfRegisterRegisterEqual, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureLoadRegisterValueInstruction(Byte register, Byte value) => _binaryRegistersOperation.Setup(_bridge.LoadRegisterValue, register, value);
        public ICPUInstruction ConfigureAddRegisterValueInstruction(Byte register, Byte value) => _binaryRegistersOperation.Setup(_bridge.AddRegisterValue, register, value);
        public ICPUInstruction ConfigureSkipIfNotRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => _binaryRegistersOperation.Setup(_bridge.SkipIfNotRegisterRegisterEqual, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureLoadRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => _binaryRegistersOperation.Setup(_bridge.LoadRegisterRegister, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureOrRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => _binaryRegistersOperation.Setup(_bridge.OrRegisterRegister, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureAndRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => _binaryRegistersOperation.Setup(_bridge.AndRegisterRegister, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureXorRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => _binaryRegistersOperation.Setup(_bridge.XorRegisterRegister, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureAddRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => _binaryRegistersOperation.Setup(_bridge.AddRegisterRegister, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureSubRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => _binaryRegistersOperation.Setup(_bridge.SubRegisterRegister, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureShrRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => _binaryRegistersOperation.Setup(_bridge.ShrRegisterRegister, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureSubnRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => _binaryRegistersOperation.Setup(_bridge.SubnRegisterRegister, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureShlRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1) => _binaryRegistersOperation.Setup(_bridge.ShlRegisterRegister, registerNumber0, registerNumber1);
        public ICPUInstruction ConfigureLoadAddressRegisterInstruction(Int16 value) => _addressOperation.Setup(_bridge.LoadAddressRegister, value);
        public ICPUInstruction ConfigureJumpAddressRegisterInstruction(Int16 value) => _addressOperation.Setup(_bridge.JumpAddressRegister, value);
        public ICPUInstruction ConfigureGenerateRandomNumberInstruction(Byte registerNum, Byte value) => _binaryRegistersOperation.Setup(_bridge.GenerateRandomNumber, registerNum, value);
        public ICPUInstruction ConfigureDrawSpriteInstruction(Byte registerNum0, Byte staff) => _binaryRegistersOperation.Setup(_bridge.DrawSprite, registerNum0, staff);
        public ICPUInstruction ConfigureSkipIfKeyPressedInstruction(Byte keyCode) => _unrayOperation.Setup(_bridge.SkipIfKeyPressed, keyCode);
        public ICPUInstruction ConfigureSkipIfKeyNotPressedInstruction(Byte keyCode) => _unrayOperation.Setup(_bridge.SkipIfKeyNotPressed, keyCode);
        public ICPUInstruction ConfigureLoadRegisterDelayTimerInstruction(Byte registerNumber) => _unrayOperation.Setup(_bridge.LoadRegisterDelayTimer, registerNumber);
        public ICPUInstruction ConfigureWaitKeyPressedInstruction(Byte keyCode) => _unrayOperation.Setup(_bridge.WaitKeyPressed, keyCode);
        public ICPUInstruction ConfigureLoadDelayTimerInstruction(Byte registerNumber) => _unrayOperation.Setup(_bridge.LoadDelayTimer, registerNumber);
        public ICPUInstruction ConfigureLoadSoundCounterInstruction(Byte registerNumber) => _unrayOperation.Setup(_bridge.LoadSoundCounter, registerNumber);
        public ICPUInstruction ConfigureAddAddressRegisterRegisterInstruction(Byte registerNumber) => _unrayOperation.Setup(_bridge.AddAddressRegisterRegister, registerNumber);
        public ICPUInstruction ConfigureLoadSymbolSpriteInstruction(Byte spriteNum) => _unrayOperation.Setup(_bridge.LoadSymbolSprite, spriteNum);
        public ICPUInstruction ConfigureLoadRegistersBCDValueInstruction(Byte registerNum) => _unrayOperation.Setup(_bridge.LoadRegisterBCD, registerNum);
        public ICPUInstruction ConfigureLoadRegistersValueInstruction(Byte registerNumber) => _unrayOperation.Setup(_bridge.LoadRegistersValue, registerNumber);
        public ICPUInstruction ConfigureLoadValueRegistersInstruction(Byte registerNumber) => _unrayOperation.Setup(_bridge.LoadValueRegisters, registerNumber);
    }
}
