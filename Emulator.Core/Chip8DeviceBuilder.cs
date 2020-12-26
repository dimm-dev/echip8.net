using System;

using Emulator.Core.Interfaces.Periphery;
using Emulator.Core.Interfaces.Units;
using Emulator.Core.Interfaces.Units.DecoderHelpers;
using Emulator.Core.Units.DecoderHelpers;
using Emulator.Core.Units;

namespace Emulator.Core
{
    public class Chip8DeviceBuilder : IDisposable
    {
        private const int FontOffset = 0;
        private static readonly Byte[] Fontset =
        {
            0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
            0x20, 0x60, 0x20, 0x20, 0x70, // 1
            0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
            0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
            0x90, 0x90, 0xF0, 0x10, 0x10, // 4
            0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
            0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
            0xF0, 0x10, 0x20, 0x40, 0x40, // 7
            0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
            0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
            0xF0, 0x90, 0xF0, 0x90, 0x90, // A
            0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
            0xF0, 0x80, 0x80, 0x80, 0xF0, // C
            0xE0, 0x90, 0x90, 0x90, 0xE0, // D
            0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
            0xF0, 0x80, 0xF0, 0x80, 0x80  // F
        };
        public IChip8Device Build(IKeyboard keyboard, IDisplay display)
        {
            IArithmeticLogicUnit alu = BuildALU();
            IMemoryManagementUnit mmu = new MemoryManagementUnit();
            ISystemBridge systemBridge = BuildBridge(mmu, alu, display, keyboard);
            ICentralProcessingUnit cpu = BuildCPU(alu, mmu, systemBridge);

            Chip8Device device = new Chip8Device(cpu, mmu, systemBridge, keyboard, display);
            LoadFonts(mmu, Fontset, FontOffset);

            return device;
        }

        public void Dispose() {}

        private IArithmeticLogicUnit BuildALU()
        {
            IAddressRegister addressRegister = new AddressRegister();
            IGeneralPurposeRegisters generalPurposeRegisters = new GeneralPurposeRegisters();
            IInstructionPointerRegister ipr = new InstructionPointerRegister();
            IReturnStack returnStack = new ReturnStack();

            IArithmeticLogicUnit alu = new ArithmeticLogicUnit(
                addressRegister,
                generalPurposeRegisters,
                ipr,
                returnStack
            );

            return alu;
        }

        private ICentralProcessingUnit BuildCPU(IArithmeticLogicUnit alu, IMemoryManagementUnit mmu, ISystemBridge bridge)
        {
            IInstructionConfigurator configurator = new InstructionConfigurator(bridge);
            IInstructionParser parser = new InstructionParser(configurator);
            InstructionReader reader = new InstructionReader(mmu, alu.IP);
            ICentralProcessingUnit cpu = new CentralProcessingUnit(alu, parser, reader);
            return cpu;
        }

        private ISystemBridge BuildBridge(IMemoryManagementUnit mmu, IArithmeticLogicUnit alu, IDisplay display, IKeyboard keyboard)
        {
            ISystemBridge bridge = new SystemBridge(mmu, alu, display, keyboard);
            return bridge;
        }

        private void LoadFonts(IMemoryManagementUnit mmu, Byte[] fonts, int fontsOffset)
        {
            for (int i = 0; i < fonts.Length; i++)
                mmu[i + fontsOffset] = fonts[i];
        }
    }
}
