using System;
using Xunit;

using Emulator.Core.Units;

namespace Emulator.Core.Tests
{
    public class AddressRegisterTests
    {
        class AddressOverflowException : Exception
        {
            public AddressOverflowException(Int16 val) => value = val;

            public Int16 value;
        }

        AddressRegister MakeAddressRegister()
        {
            var ar = new AddressRegister();
            ar.AddressRegisterInvalidAddress += (Int16 val) => throw new AddressOverflowException(val);
            return ar;
        }

        [Fact]
        public void AddressRegisterGetSetTests()
        {
            AddressRegister arg = MakeAddressRegister();
            Assert.True(arg.Value == 0);
            arg.Value = 6;
            Assert.True(arg.Value == 6);
        }

        [Fact]
        public void AddressRegisterOverflowTests()
        {
            AddressRegister arg = MakeAddressRegister();
            arg.Value = 4;
            Assert.Throws<AddressOverflowException>(() => arg.Value = 0x1000);
            Assert.True(arg.Value == 4);
        }
    }
}
