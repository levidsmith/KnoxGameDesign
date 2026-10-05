RGBDS=/d/ldsmith/presentations/gameboy/rgbds-win64

gameboy01.gb: gameboy01.asm knox.chr
	${RGBDS}/rgbasm -o gameboy01.o gameboy01.asm
	${RGBDS}/rgblink -o gameboy01.gb gameboy01.o
	${RGBDS}/rgbfix -v -p 0xFF gameboy01.gb
clean:
	rm -f *.o
	rm -f *.gb
