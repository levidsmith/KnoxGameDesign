# 2026 Levi D. Smith <developer@levidsmith.com>
# based on https://github.com/alexras/truepeacein.space
# by alexras

import math

from bit_buffer import BitBuffer

maru_password = "0G0000 000000 400000 00000H"

PASSWORD_SIZE_BYTES = 18
METROID_ALPHABET = '0123456789ABCDEFGHILJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz?-'

def passwordStringToMetroidAlphabet(passwordString):
  passwordBytes = []

  currentChar = passwordString[i]

def bufferToPasswordCharacters(buffer):
  print(f"{buffer.getBits(0, 6):06b}")
  print(f"{buffer.getBits(6, 6):06b}")
  print(f"{buffer.getBits(12, 6):06b}")
  print(f"{buffer.getBits(18, 6):06b}")
#  for i in range(int(PASSWORD_SIZE_BYTES * 8 / 6)):
#    print(f"{buffer.getBits(int(i / 6), 6):06b}")
  for i in range(int(PASSWORD_SIZE_BYTES * 8 / 6)):
    print(i * 6) 
    bit = i * 6
    print(f"{buffer.getBits(bit, 6):06b}")
buffer = BitBuffer()

MARU_MARI_BIT = 76
buffer.setBit(MARU_MARI_BIT, True)


#buffer.setBit(8, True)
#buffer.setBit(9, True)
#buffer.setBit(10, True)
#buffer.setBit(9, False)

buffer.printBuffer()

print("checksum: " + str(buffer.calcChecksum()))

print(f"getBits(72, 6):  {buffer.getBits(72, 6):06b}")


buffer.setBit(7, True)
buffer.setBit(8, True)
buffer.printBuffer()
print("checksum: " + str(buffer.calcChecksum()))
print(f"getBits(6, 6):  {buffer.getBits(6, 6):06b}")

print(f"7: {buffer.getBit(7)}")
print(f"8: {buffer.getBit(8)}")
print(f"9: {buffer.getBit(9)}")
print(f"72: {buffer.getBit(72)}")
print(f"76: {buffer.getBit(76)}")

bufferToPasswordCharacters(buffer)
