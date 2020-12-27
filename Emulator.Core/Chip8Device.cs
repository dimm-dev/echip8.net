using Emulator.Core.Interfaces.Periphery;
using Emulator.Core.Interfaces.Units;

namespace Emulator.Core
{
    public class Chip8Device : IChip8Device
    {
        public ICentralProcessingUnit CPU { get; }
        public IMemoryManagementUnit MMU { get; }
        public ISystemBridge Bridge { get; }
        public IKeyboardDataSource Keyboard { get; }
        public IDisplay Display { get; }

        public Chip8Device(
            ICentralProcessingUnit cpu,
            IMemoryManagementUnit mmu,
            ISystemBridge systemBridge,
            IKeyboardDataSource keyboard,
            IDisplay display)

        {
            // TODO: subscribe to device events
            CPU = cpu;
            MMU = mmu;
            Bridge = systemBridge;
            Keyboard = keyboard;
            Display = display;
        }

        public void Reset()
        {
            Display.Clear();
            CPU.Reset();
        }

        public void Tick() => CPU.Tick();
    }
}
