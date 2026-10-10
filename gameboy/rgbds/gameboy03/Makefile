RGBDS=/d/ldsmith/presentations/gameboy/rgbds-win64

gameboy03.gb: gameboy03.asm knox.chr
	${RGBDS}/rgbasm -o gameboy03.o gameboy03.asm
	${RGBDS}/rgblink -o gameboy03.gb gameboy03.o
	${RGBDS}/rgbfix -v -p 0xFF gameboy03.gb
clean:
	rm -f *.o
	rm -f *.gb
