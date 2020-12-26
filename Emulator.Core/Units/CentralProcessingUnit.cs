using System;

using Emulator.Core.Interfaces.Instructions;
using Emulator.Core.Interfaces.Units;
using Emulator.Core.Interfaces.Units.DecoderHelpers;

namespace Emulator.Core.Units
{
    public class CentralProcessingUnit : ICentralProcessingUnit
    {
        public IArithmeticLogicUnit ALU { get; }
        public IInstructionParser Parser { get; }
        private IInstructionReader Reader { get; set; }

        public CentralProcessingUnit(IArithmeticLogicUnit arithmeticLogicUnit, IInstructionParser parser, IInstructionReader reader)
        {
            ALU = arithmeticLogicUnit;
            Parser = parser;
            Reader = reader;
        }

        public void Tick()
        {
            if (Reader.MoveNext() == true)
            {
                Int16 instructionCode = Reader.Current;
                ICPUInstruction instruction = Parser.Parse(instructionCode);
                instruction.Execute();
            }
        }

        public void Reset()
        {
            ALU.Reset();
            Reader.Reset();
        }
    }
}
