using System;

namespace Emulator.Core.Interfaces.Units
{
    public interface IAddressRegister
    {
        Int16 Value { get; set; }
        event Action<Int16> AddressRegisterInvalidAddress;
    }
}
