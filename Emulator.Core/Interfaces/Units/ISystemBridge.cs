using System;

namespace Emulator.Core.Interfaces.Units
{
    public interface ISystemBridge
    {
        // 0
        void ClearScreen();
        void Return();
        // 1, 2
        void Jump(Int16 address);
        void Call(Int16 address);
        // 3, 4
        void SkipIfRegisterValueEqual(Byte register, Byte value);
        void SkipIfNotRegisterValueEqual(Byte register, Byte value);
        // 5, 6, 7
        void SkipIfRegisterRegisterEqual(Byte ro0, Byte r1);
        void LoadRegisterValue(Byte register, Byte value);
        void AddRegisterValue(Byte registerNum, Byte value);
        // 8
        void LoadRegisterRegister(Byte r0, Byte r1);
        void OrRegisterRegister(Byte r0, Byte r1);
        void AndRegisterRegister(Byte r0, Byte r1);
        void XorRegisterRegister(Byte r0, Byte r1);
        void AddRegisterRegister(Byte r0, Byte r1);
        void SubRegisterRegister(Byte r0, Byte r1);
        void ShrRegisterRegister(Byte r0, Byte r1);
        void SubnRegisterRegister(Byte r0, Byte r1);
        void ShlRegisterRegister(Byte r0, Byte r1);
        // 9
        void SkipIfNotRegisterRegisterEqual(Byte r0, Byte r1);
        // 10, 11
        void LoadAddressRegister(Int16 value);
        void JumpAddressRegister(Int16 value);
        // 12
        void GenerateRandomNumber(Byte r0, Byte value);
        // 13
        // TODO: 'staff' to ry and count
        void DrawSprite(Byte rx, Byte staff);
        // 14
        void SkipIfKeyPressed(Byte keyCode);
        void SkipIfKeyNotPressed(Byte keyCode);
        // 15
        void LoadRegisterDelayTimer(Byte registerNum);
        void WaitKeyPressed(Byte keyCode);
        void LoadDelayTimer(Byte registerNum);
        void LoadSoundCounter(Byte registerNum);
        void AddAddressRegisterRegister(Byte registerNum);
        void LoadSymbolSprite(Byte spriteNum);
        void LoadRegisterBCD(Byte registerNum);
        void LoadRegistersValue(Byte registerNum);
        void LoadValueRegisters(Byte registerNum);
    }
}
