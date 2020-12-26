using System;
using System.Linq;
using Xunit;

using Emulator.Core.Units;

namespace Emulator.Core.Tests
{
    public class ReturnStackTests
    {
        private class StackOverflowException : Exception
        {
        }

        private class StackUnderflowException : Exception
        {
        }

        ReturnStack MakeStack(int size)
        {
            ReturnStack stack = new ReturnStack(size);
            stack.StackOverflow += () => throw new StackOverflowException();
            stack.StackUnderflow += () => throw new StackUnderflowException();

            return stack;
        }

        [Fact]
        void ReturnStackSizeAndAlignmentTest()
        {
            Assert.Throws<ArgumentException>(() => MakeStack(-1));
            Assert.Throws<ArgumentException>(() => MakeStack(0));
            Assert.Throws<ArgumentException>(() => MakeStack(3));
            var stack = MakeStack(4);
            Assert.True(stack.Size == 4);
            Assert.True(stack.Position == 4);
        }

        [Fact]
        void ReturnStackPushPopTest()
        {
            Int16[] data = { 0, 1, 2, 3, 4, 5 };
            ReturnStack rs = MakeStack(data.GetLength(0));
            data.Any(i => { rs.Push(data[i]); return false; });
            Assert.True(rs.Position == 0);
            Assert.True(data.All(i => rs.Pop() == data[rs.Size - rs.Position]));
        }

        [Fact]
        void ReturnStackOverflowTest()
        {
            Int16[] data = { 0, 1, 2, 3, 4 };
            ReturnStack rs = MakeStack(data.GetLength(0) - 1);
            Assert.Throws<StackOverflowException>(() => data.Any(i => { rs.Push(data[i]); return false; }));
        }

        [Fact]
        void ReturnStackUnderflowTest()
        {
            Int16[] data = { 0, 1, 2, 3, 4, 5 };
            ReturnStack rs = MakeStack(data.GetLength(0));
            data.Any(i => { rs.Push(data[i]); return false; });
            Assert.Throws<StackUnderflowException>(() => { data.Any(i => { rs.Pop(); return false; }); rs.Pop(); });
        }
    }
}
