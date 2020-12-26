using System;

namespace Emulator.Core.Interfaces.Units
{
    public interface IMemoryManagementUnit
    {
        int Size { get; }
        Byte this[int i] { get; set; }
    }
}
