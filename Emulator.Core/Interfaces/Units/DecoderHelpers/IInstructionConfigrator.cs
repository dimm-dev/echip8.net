using System;

using Emulator.Core.Interfaces.Instructions;

namespace Emulator.Core.Interfaces.Units.DecoderHelpers
{
    public interface IInstructionConfigurator
    {
        ICPUInstruction ConfigureClearScreenInstruction();
        ICPUInstruction ConfigureReturnInstruction();
        ICPUInstruction ConfigureJumpInstruction(Int16 address);
        ICPUInstruction ConfigureCallInstruction(Int16 address);
        ICPUInstruction ConfigureSkipIfRegisterValueInstruction(Byte registerNumber, Byte value);
        ICPUInstruction ConfigureSkipIfNotRegisterValueInstruction(Byte registerNumber, Byte value);
        ICPUInstruction ConfigureSkipIfRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1);
        ICPUInstruction ConfigureLoadRegisterValueInstruction(Byte startRegister, Byte value);
        ICPUInstruction ConfigureAddRegisterValueInstruction(Byte startRegister, Byte value);
        ICPUInstruction ConfigureSkipIfNotRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1);
        ICPUInstruction ConfigureLoadRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1);
        ICPUInstruction ConfigureOrRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1);
        ICPUInstruction ConfigureAndRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1);
        ICPUInstruction ConfigureXorRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1);
        ICPUInstruction ConfigureAddRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1);
        ICPUInstruction ConfigureSubRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1);
        ICPUInstruction ConfigureShrRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1);
        ICPUInstruction ConfigureSubnRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1);
        ICPUInstruction ConfigureShlRegisterRegisterInstruction(Byte registerNumber0, Byte registerNumber1);
        ICPUInstruction ConfigureLoadAddressRegisterInstruction(Int16 value);
        ICPUInstruction ConfigureJumpAddressRegisterInstruction(Int16 value);
        ICPUInstruction ConfigureGenerateRandomNumberInstruction(Byte registerNumber0, Byte value);
        ICPUInstruction ConfigureDrawSpriteInstruction(Byte registerNumber0, Byte staff);
        ICPUInstruction ConfigureSkipIfKeyPressedInstruction(Byte keyCode);
        ICPUInstruction ConfigureSkipIfKeyNotPressedInstruction(Byte keyCode);
        ICPUInstruction ConfigureLoadRegisterDelayTimerInstruction(Byte registerNumber);
        ICPUInstruction ConfigureWaitKeyPressedInstruction(Byte keyCode);
        ICPUInstruction ConfigureLoadDelayTimerInstruction(Byte registerNumber);
        ICPUInstruction ConfigureLoadSoundCounterInstruction(Byte registerNumber);
        ICPUInstruction ConfigureAddAddressRegisterRegisterInstruction(Byte registerNumber);
        ICPUInstruction ConfigureLoadSymbolSpriteInstruction(Byte spriteNum);
        ICPUInstruction ConfigureLoadRegistersBCDValueInstruction(Byte registerNum);
        ICPUInstruction ConfigureLoadRegistersValueInstruction(Byte registerNumber);
        ICPUInstruction ConfigureLoadValueRegistersInstruction(Byte registerNumber);
    }
}
