DASM_PATH=./dasm-2.20.14.1-win-x64
Z26_PATH=./z26_407_win10

all: out/atari00.a26 out/atari01.a26 out/atari02.a26

out/atari00.a26: src/atari00/atari00.asm
	${DASM_PATH}/dasm src/atari00/atari00.asm -I${DASM_PATH}/machines/atari2600 -lout/atari00.txt -f3 -v5 -oout/atari00.a26

out/atari01.a26: src/atari01/atari01.asm
	${DASM_PATH}/dasm src/atari01/atari01.asm -I${DASM_PATH}/machines/atari2600 -lout/atari01.txt -f3 -v5 -oout/atari01.a26

out/atari02.a26: src/atari02/atari02.asm src/atari02/knox.asm
	${DASM_PATH}/dasm src/atari02/atari02.asm -I${DASM_PATH}/machines/atari2600 -lout/atari02.txt -f3 -v5 -oout/atari02.a26

run00:
	${Z26_PATH}/z26 out/atari00.a26

run01:
	${Z26_PATH}/z26 out/atari01.a26

run02:
	${Z26_PATH}/z26 out/atari02.a26

clean:
	rm -f out/*.a26 out/*.txt
