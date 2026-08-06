set DASM_PATH=.\dasm-2.20.14.1-win-x64
set Z26_PATH=.\z26_407_win10

%DASM_PATH%\dasm src\atari00\atari00.asm -I%DASM_PATH%\machines\atari2600 -lout\atari00.txt -f3 -v5 -oout\atari00.a26
rem %Z26_PATH%\z26 atari00.a26

%DASM_PATH%\dasm src\atari01\atari01.asm -I%DASM_PATH%\machines\atari2600 -lout\atari01.txt -f3 -v5 -oout\atari01.a26
rem %Z26_PATH%\z26 atari00.a26