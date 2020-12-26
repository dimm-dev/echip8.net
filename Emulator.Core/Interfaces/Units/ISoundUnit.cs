using System;

namespace chip8.core.interfaces
{
    public interface ISoundUnit
    {
        Byte Count { get; set; }
        void Tick();
    }
}
