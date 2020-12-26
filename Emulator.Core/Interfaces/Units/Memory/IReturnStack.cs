using System;

namespace Emulator.Core.Interfaces.Units
{
    public interface IReturnStack
    {
        int Size { get; }
        int Position { get; }
        void Push(Int16 address);
        Int16 Pop();
        void Reset();

        event Action StackOverflow;
        event Action StackUnderflow;
    }
}
