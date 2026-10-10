RGBDS=/d/ldsmith/presentations/gameboy/rgbds-win64

gameboy00.gb: gameboy00.asm knox.chr
	${RGBDS}/rgbasm -o gameboy00.o gameboy00.asm
	${RGBDS}/rgblink -o gameboy00.gb gameboy00.o
	${RGBDS}/rgbfix -v -p 0xFF gameboy00.gb
clean:
	rm -f *.o
	rm -f *.gb
