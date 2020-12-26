using System;
using Emulator.Core.Interfaces.Units;

namespace Emulator.Core.Units
{
    public class ReturnStack : IReturnStack
    {
        public const int DefaultStackSize = 16;

        private Int16[] _stack;
        private int _pos;

        public ReturnStack(int size = DefaultStackSize)
        {
            if ((size & 1) != 0 || size <= 0)
                throw new ArgumentException($"GeneralReturnStack: given stack size of {size} is invalid. Must be positive and odd", "size");

            _stack = new Int16[size];
            _pos = 0;
        }

        public int Size { get => _stack.GetLength(0); }
        public int Position { get => Size - _pos; }

        public void Push(Int16 address)
        {
            if (_pos < Size)
            {
                _stack[_pos] = address;
                _pos++;
            }
            else
            {
                StackOverflow();
            }
        }

        public Int16 Pop()
        {
            Int16 val = 0;
            if (_pos > 0)
            {
                _pos--;
                val = _stack[_pos];
            }
            else
            {
                val = _stack[0];
                StackUnderflow();
            }
            return val;
        }

        public void Reset() => _pos = 0;

        public event Action StackOverflow = delegate {};
        public event Action StackUnderflow = delegate {};
    }
}
