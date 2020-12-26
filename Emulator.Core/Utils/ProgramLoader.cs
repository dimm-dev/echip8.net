using System;
using System.IO;
using Emulator.Core.Interfaces.Units;

namespace Emulator.Core.Utils
{
    public class ProgramLoader : IDisposable
    {
        private IChip8Device _device;

        public ProgramLoader(IChip8Device device) => _device = device;

        public void Load(Stream source)
        {
            int start = _device.CPU.ALU.IP.Start;
            int end = _device.MMU.Size;
            System.Byte[] buffer = new System.Byte[end - start + 1];
            int nr = source.Read(buffer, 0, buffer.Length);
            nr = System.Math.Min(nr, buffer.Length);
            for (int i = 0; i < nr; i++)
                _device.MMU[i + start] = buffer[i];

            // TODO: byte-oriented program loading. Is it function of the device itself?
            _device.CPU.ALU.IP.UpdateRange((Int16)start, (Int16)(start + nr - 1));
        }

        public void Dispose() {}
    }
}
