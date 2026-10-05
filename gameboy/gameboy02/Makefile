RGBDS=/d/ldsmith/presentations/gameboy/rgbds-win64

gameboy02.gb: gameboy02.asm knox.chr
	${RGBDS}/rgbasm -o gameboy02.o gameboy02.asm
	${RGBDS}/rgblink -o gameboy02.gb gameboy02.o
	${RGBDS}/rgbfix -v -p 0xFF gameboy02.gb
clean:
	rm -f *.o
	rm -f *.gb
