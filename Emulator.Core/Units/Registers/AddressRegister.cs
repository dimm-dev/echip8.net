using System;
using Emulator.Core.Interfaces.Units;

namespace Emulator.Core.Units
{
    public class AddressRegister : IAddressRegister
    {
        private Int16 _value = 0;

        public AddressRegister()
        {
        }

        public Int16 Value { get => _value; set => _value = SetValueByMask(value); }

        private Int16 SetValueByMask(Int16 value)
        {
            if ((value & 0xF000) != 0)
            {
                AddressRegisterInvalidAddress?.Invoke(value);
                return _value;
            }

            return value;
        }

        public event Action<Int16> AddressRegisterInvalidAddress;
    }
}
