using Emulator.Core.Interfaces.Periphery;

namespace Emulator.Core.Interfaces.Units
{
    public interface IChip8Device
    {
        ICentralProcessingUnit CPU { get; }
        IMemoryManagementUnit MMU { get; }
        ISystemBridge Bridge { get; }
        IKeyboard Keyboard { get; }
        IDisplay Display { get; }

        void Reset();

        void Tick();
    }
}
