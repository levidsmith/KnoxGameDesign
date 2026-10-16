RGBDS=/d/ldsmith/presentations/gameboy/rgbds-win64

default: gameboy00.gb gameboy00_light.gb

gameboy00.gb: gameboy00.asm knox.chr
	${RGBDS}/rgbasm -o gameboy00.o gameboy00.asm
	${RGBDS}/rgblink -o gameboy00.gb gameboy00.o
	${RGBDS}/rgbfix -v -p 0xFF gameboy00.gb

gameboy00_light.gb: gameboy00_light.asm knox_light.chr
	${RGBDS}/rgbasm -o gameboy00_light.o gameboy00_light.asm
	${RGBDS}/rgblink -o gameboy00_light.gb gameboy00_light.o
	${RGBDS}/rgbfix -v -p 0xFF gameboy00_light.gb

clean:
	rm -f *.o
	rm -f *.gb
