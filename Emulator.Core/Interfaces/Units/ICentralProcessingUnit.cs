namespace Emulator.Core.Interfaces.Units
{
    public interface ICentralProcessingUnit
    {
        IArithmeticLogicUnit ALU { get; }

        void Tick();
        void Reset();
    }
}
