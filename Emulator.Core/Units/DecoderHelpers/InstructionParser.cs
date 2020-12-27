using System;
using System.Collections.Generic;

using Emulator.Core.Interfaces.Instructions;
using Emulator.Core.Interfaces.Units.DecoderHelpers;

namespace Emulator.Core.Units.DecoderHelpers
{
    public class InstructionParser : IInstructionParser
    {
        private delegate ICPUInstruction ProcessFunc(InstructionComponents instruction, IInstructionConfigurator configurator, Action<Int16> unknownInstructionHandler);

        private delegate ICPUInstruction ConfigureBinaryOperationDelegate(IInstructionConfigurator configurator, Byte registerNum0, Byte registerNum1);

        private static readonly ConfigureBinaryOperationDelegate[]  _code8Instructions = new ConfigureBinaryOperationDelegate[]
        {
            (configurator, reg0, reg1) => { return configurator.ConfigureLoadRegisterRegisterInstruction(reg0, reg1); },
            (configurator, reg0, reg1) => { return configurator.ConfigureOrRegisterRegisterInstruction(reg0, reg1); },
            (configurator, reg0, reg1) => { return configurator.ConfigureAndRegisterRegisterInstruction(reg0, reg1); },
            (configurator, reg0, reg1) => { return configurator.ConfigureXorRegisterRegisterInstruction(reg0, reg1); },
            (configurator, reg0, reg1) => { return configurator.ConfigureAddRegisterRegisterInstruction(reg0, reg1); },
            (configurator, reg0, reg1) => { return configurator.ConfigureSubRegisterRegisterInstruction(reg0, reg1); },
            (configurator, reg0, reg1) => { return configurator.ConfigureShrRegisterRegisterInstruction(reg0, reg1); },
            (configurator, reg0, reg1) => { return configurator.ConfigureSubnRegisterRegisterInstruction(reg0, reg1); },
            null,
            null,
            null,
            null,
            null,
            null,
            (configurator, reg0, reg1) => { return configurator.ConfigureShlRegisterRegisterInstruction(reg0, reg1); }
        };

        private static readonly ProcessFunc[] _processFunctions = new ProcessFunc[]
        {
            ProcessCode0,
            (instruction, configurator, handler) => { return configurator.ConfigureJumpInstruction(instruction.Value); },
            (instruction, configurator, handler) => { return configurator.ConfigureCallInstruction(instruction.Value); },
            (instruction, configurator, handler) => { return configurator.ConfigureSkipIfRegisterValueInstruction(instruction.HighNibbles.Low, instruction.Octets.Low); },
            (instruction, configurator, handler) => { return configurator.ConfigureSkipIfNotRegisterValueInstruction(instruction.HighNibbles.Low, instruction.Octets.Low); },
            (instruction, configurator, handler) => { return configurator.ConfigureSkipIfRegisterRegisterInstruction(instruction.HighNibbles.Low, instruction.LowNibbles.High); },
            (instruction, configurator, handler) => { return configurator.ConfigureLoadRegisterValueInstruction(instruction.HighNibbles.Low, instruction.Octets.Low); },
            (instruction, configurator, handler) => { return configurator.ConfigureAddRegisterValueInstruction(instruction.HighNibbles.Low, instruction.Octets.Low); },
            ProcessCode8,
            (instruction, configurator, handler) => { return configurator.ConfigureSkipIfNotRegisterRegisterInstruction(instruction.HighNibbles.Low, instruction.LowNibbles.High); },
            (instruction, configurator, handler) => { return configurator.ConfigureLoadAddressRegisterInstruction(instruction.Value); },
            (instruction, configurator, handler) => { return configurator.ConfigureJumpAddressRegisterInstruction(instruction.Value); },
            (instruction, configurator, handler) => { return configurator.ConfigureGenerateRandomNumberInstruction(instruction.HighNibbles.Low, instruction.Octets.Low); },
            (instruction, configurator, handler) => { return configurator.ConfigureDrawSpriteInstruction(instruction.HighNibbles.Low, instruction.Octets.Low); },
            ProcessCodeE,
            ProcessCodeF
        };

        private readonly IInstructionConfigurator _configurator;
        private InstructionComponents _instructionParts = new InstructionComponents();

        public event Action<Int16> UnknownInstructionFoundEvent;

        public InstructionParser(IInstructionConfigurator configurator) => _configurator = configurator;

        public ICPUInstruction Parse(Int16 instructionCode)
        {
            ProcessFunc processor = ProcessUnknownInstruction;

            _instructionParts.SetValue(instructionCode);

            if (_instructionParts.Code >= 0 && _instructionParts.Code < _processFunctions.Length)
                processor = _processFunctions[_instructionParts.Code];

            return processor(_instructionParts, _configurator, UnknownInstructionFoundEvent);
        }

        private static ICPUInstruction ProcessUnknownInstruction(InstructionComponents instructionParts, IInstructionConfigurator configurator, Action<Int16> unknownInstructionHandler)
        {
            unknownInstructionHandler?.Invoke((Int16)(instructionParts.Octets.Low << 8 | instructionParts.Octets.Low));
            return null;
        }

        private static ICPUInstruction ProcessCode0(InstructionComponents instructionParts, IInstructionConfigurator configurator, Action<Int16> unknownInstructionHandler)
        {
            ICPUInstruction instruction = null;

            if (instructionParts.Octets.Low == 0xE0)
                instruction = configurator.ConfigureClearScreenInstruction();
            else if (instructionParts.Octets.Low == 0xEE)
                instruction = configurator.ConfigureReturnInstruction();
            else
                instruction = ProcessUnknownInstruction(instructionParts, configurator, unknownInstructionHandler);

            return instruction;
        }

        private static ICPUInstruction ProcessCode8(InstructionComponents instructionParts, IInstructionConfigurator configurator, Action<Int16> unknownInstructionHandler)
        {
            int subCode = instructionParts.LowNibbles.Low;

            if (subCode < 0 || subCode >= _code8Instructions.Length || _code8Instructions[subCode] == null)
                return ProcessUnknownInstruction(instructionParts, configurator, unknownInstructionHandler);

            ConfigureBinaryOperationDelegate configDelegate = _code8Instructions[subCode];

            return configDelegate(configurator, instructionParts.HighNibbles.Low, instructionParts.LowNibbles.High);
        }

        private static ICPUInstruction ProcessCodeE(InstructionComponents instructionParts, IInstructionConfigurator configurator, Action<Int16> unknownInstructionHandler)
        {
            int subCode = instructionParts.Octets.Low;
            ICPUInstruction instruction = null;
            if (subCode == 0x9E)
                instruction = configurator.ConfigureSkipIfKeyPressedInstruction(instructionParts.HighNibbles.Low);
            else if (subCode == 0xA1)
                instruction = configurator.ConfigureSkipIfKeyNotPressedInstruction(instructionParts.HighNibbles.Low);
            else
                instruction = ProcessUnknownInstruction(instructionParts, configurator, unknownInstructionHandler);

            return instruction;
        }

        private static ICPUInstruction ProcessCodeF(InstructionComponents instructionParts, IInstructionConfigurator configurator, Action<Int16> unknownInstructionHandler)
        {
            int subCode = instructionParts.Octets.Low;
            ICPUInstruction instruction = null;
            Byte arg = instructionParts.HighNibbles.Low;
            switch (subCode)
            {
                case 0x07: instruction = configurator.ConfigureLoadRegisterDelayTimerInstruction(arg); break;
                case 0x0A: instruction = configurator.ConfigureWaitKeyPressedInstruction(arg); break;
                case 0x15: instruction = configurator.ConfigureLoadDelayTimerInstruction(arg); break;
                case 0x18: instruction = configurator.ConfigureLoadSoundCounterInstruction(arg); break;
                case 0x1E: instruction = configurator.ConfigureAddAddressRegisterRegisterInstruction(arg); break;
                case 0x29: instruction = configurator.ConfigureLoadSymbolSpriteInstruction(arg); break;
                case 0x33: instruction = configurator.ConfigureLoadRegistersBCDValueInstruction(arg); break;
                case 0x55: instruction = configurator.ConfigureLoadRegistersValueInstruction(arg); break;
                case 0x65: instruction = configurator.ConfigureLoadValueRegistersInstruction(arg); break;
                default: instruction = ProcessUnknownInstruction(instructionParts, configurator, unknownInstructionHandler); break;
            }

            return instruction;
        }
    }
}
