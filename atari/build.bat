set DASM_PATH=.\dasm-2.20.14.1-win-x64
set Z26_PATH=.\z26_407_win10

%DASM_PATH%\dasm src\atari00\test.asm -I%DASM_PATH%\machines\atari2600 -ltest.txt -f3 -v5 -oatari00.a26
%Z26_PATH%\z26 atari00.a26