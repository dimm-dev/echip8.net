using System;

using Emulator.Core.Interfaces.Periphery;
using Emulator.Core.Interfaces.Units;

namespace Emulator.Core.Units
{
    public class SystemBridge : ISystemBridge
    {
        private const int MaxSpriteSize = 8 * 15 + 8;
        private IMemoryManagementUnit _mmu;
        private IArithmeticLogicUnit _alu;
        private IKeyboard _keyboard;
        private IDisplay _display;
        private Random _rand = new Random();
        private Byte[] _spriteBuffer;

        public SystemBridge(IMemoryManagementUnit mmu, IArithmeticLogicUnit alu, IDisplay display, IKeyboard keyboard)
        {
            _mmu = mmu;
            _alu = alu;
            _display = display;
            _keyboard = keyboard;
            // TODO: calculate according to display settings?
            _spriteBuffer = new Byte[MaxSpriteSize];
        }

        // 0
        public void ClearScreen() => _display.Clear();

        public void Return() => _alu.Return();

        // 1, 2
        public void Jump(Int16 address) => _alu.Jump(address);

        public void Call(Int16 address) => _alu.Call(address);

        // 3, 4
        public void SkipIfRegisterValueEqual(Byte register, Byte value)
        {
            if (_alu.Registers[register] == value)
                _alu.Skip();
        }

        public void SkipIfNotRegisterValueEqual(Byte register, Byte value)
        {
            if (_alu.Registers[register] != value)
                _alu.Skip();
        }

        // 5, 6, 7
        public void SkipIfRegisterRegisterEqual(Byte r0, Byte r1)
        {
            if (_alu.Registers[r0] == _alu.Registers[r1])
                _alu.Skip();
        }

        public void LoadRegisterValue(Byte registerNum, Byte value) => _alu.Registers[registerNum] = value;

        public void AddRegisterValue(Byte registerNum, Byte value) => _alu.Registers[registerNum] += value;

        // 8
        public void LoadRegisterRegister(Byte r0, Byte r1) => _alu.Registers[r0] = _alu.Registers[r1];

        public void OrRegisterRegister(Byte r0, Byte r1) => _alu.Registers[r0] |= _alu.Registers[r1];

        public void AndRegisterRegister(Byte r0, Byte r1) => _alu.Registers[r0] &= _alu.Registers[r1];

        public void XorRegisterRegister(Byte r0, Byte r1) => _alu.Registers[r0] ^= _alu.Registers[r1];

        public void AddRegisterRegister(Byte r0, Byte r1)
        {
            int value = _alu.Registers[r0] + _alu.Registers[r1];
            _alu.Registers[r0] = (Byte)(value & 0xFF);
            _alu.SetOverflowFlag((value & 0xFF00) > 0);
        }

        public void SubRegisterRegister(Byte r0, Byte r1)
        {
            _alu.SetOverflowFlag(_alu.Registers[r0] > _alu.Registers[r1]);
            _alu.Registers[r0] = (Byte)(_alu.Registers[r0] - _alu.Registers[r1]);
        }

        public void ShrRegisterRegister(Byte r0, Byte r1)
        {
            _alu.SetOverflowFlag((_alu.Registers[r0] & 0x01) != 0);
            _alu.Registers[r0] >>= 1;
        }

        public void SubnRegisterRegister(Byte r0, Byte r1)
        {
            _alu.SetOverflowFlag(_alu.Registers[r1] > _alu.Registers[r0]);
            _alu.Registers[r0] = (Byte)(_alu.Registers[r1] - _alu.Registers[r0]);
        }

        public void ShlRegisterRegister(Byte r0, Byte r1)
        {
            // FIXME: only one bit shift?
            int shl = _alu.Registers[r0] << 1;
            _alu.SetOverflowFlag((shl & 0x0F00) > 0);
            _alu.Registers[r0] = (Byte)shl;
        }

        // 9
        public void SkipIfNotRegisterRegisterEqual(Byte r0, Byte r1)
        {
            if (_alu.Registers[r0] != _alu.Registers[r1])
                _alu.Skip();
        }

        // 10, 11
        public void LoadAddressRegister(Int16 value) => _alu.AddressRegister.Value = value;

        public void JumpAddressRegister(Int16 value) => _alu.Jump((Int16)(_alu.Registers[0] + value));

        // 12
        public void GenerateRandomNumber(Byte r0, Byte value) => _alu.Registers[r0] = (Byte)((_rand.Next() & 0xFF) & value);

        // 13
        public void DrawSprite(Byte rx, Byte staff)
        {
            Byte ry = (Byte)(staff >> 4);
            Byte count = (Byte)(staff & 0x0F);
            Byte x = _alu.Registers[rx];
            Byte y = _alu.Registers[ry];
            int last = _alu.AddressRegister.Value + count;
            int offset = 0;
            for (int i = _alu.AddressRegister.Value; i < last; i++)
            {
                Emulator.Core.Utils.BinaryValueConverter.GetByteRepresentation(_spriteBuffer, offset, _mmu[i]);
                offset += 8;
            }
            bool overflow = _display.DrawSpriteXOR(_spriteBuffer, offset, x, y);
            _alu.SetOverflowFlag(overflow);
        }

        // 14
        public void SkipIfKeyPressed(Byte keyCode)
        {
            if (_keyboard.IsKeyPressed(keyCode))
                _alu.Skip();
        }

        public void SkipIfKeyNotPressed(Byte keyCode)
        {
            if (_keyboard.IsKeyPressed(keyCode) == false)
                _alu.Skip();
        }

        // 15
        public void LoadRegisterDelayTimer(Byte registerNum)
        {
            
        }

        public void WaitKeyPressed(Byte keyCode)
        {
        }

        public void LoadDelayTimer(Byte registerNum)
        {
        }

        public void LoadSoundCounter(Byte registerNum)
        {
        }

        public void AddAddressRegisterRegister(Byte registerNum) => _alu.AddressRegister.Value = (Int16)(_alu.AddressRegister.Value + _alu.Registers[registerNum]);

        public void LoadSymbolSprite(Byte spriteNum)
        {
        }

        public void LoadRegisterBCD(Byte registerNum)
        {
            Int16 adr = _alu.AddressRegister.Value;
            Byte val = _alu.Registers[registerNum];

            _mmu[adr + 2] = (Byte)(val % 10);
            _mmu[adr + 1] = (Byte)((val / 10) % 10);
            _mmu[adr] = (Byte)(val / 100);
        }

        public void LoadRegistersValue(Byte registerNum)
        {
            for (int i = 0; i <= registerNum; i++)
                _mmu[_alu.AddressRegister.Value + i] = _alu.Registers[i];
        }

        public void LoadValueRegisters(Byte registerNum)
        {
            for (int i = 0; i <= registerNum; i++)
                _alu.Registers[i] = _mmu[_alu.AddressRegister.Value + i];
        }
    }
}
