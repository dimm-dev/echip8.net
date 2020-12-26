using System;

namespace Emulator.Core.Interfaces.Units
{
    public interface IGeneralPurposeRegisters
    {
        int Count { get; }
        Byte this[int num] { get; set; }
    }
}
