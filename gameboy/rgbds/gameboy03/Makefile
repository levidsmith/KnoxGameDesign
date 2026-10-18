RGBDS=/d/ldsmith/presentations/gameboy/rgbds-win64

default: gameboy03.gb gameboy03_debug.gb

gameboy03.gb: gameboy03.asm knox.chr
	${RGBDS}/rgbasm -o gameboy03.o gameboy03.asm
	${RGBDS}/rgblink -o gameboy03.gb gameboy03.o
	${RGBDS}/rgbfix -v -p 0xFF gameboy03.gb

gameboy03_debug.gb: gameboy03_debug.asm knox.chr
	${RGBDS}/rgbasm -o gameboy03_debug.o gameboy03_debug.asm
	${RGBDS}/rgblink -o gameboy03_debug.gb gameboy03_debug.o
	${RGBDS}/rgbfix -v -p 0xFF gameboy03_debug.gb
clean:
	rm -f *.o
	rm -f *.gb
