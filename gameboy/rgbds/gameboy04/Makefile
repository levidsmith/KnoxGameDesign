RGBDS=/d/ldsmith/presentations/gameboy/rgbds-win64

default: gameboy04.gb

gameboy04.gb: gameboy04.asm ldsmith.chr
	${RGBDS}/rgbasm -o gameboy04.o gameboy04.asm
	${RGBDS}/rgblink -o gameboy04.gb gameboy04.o
	${RGBDS}/rgbfix -v -p 0xFF gameboy04.gb

clean:
	rm -f *.o
	rm -f *.gb
