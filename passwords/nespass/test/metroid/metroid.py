# 2026 Levi D. Smith <developer@levidsmith.com>

from bit_buffer import BitBuffer

maru_password = "0G0000 000000 400000 00000H"

PASSWORD_SIZE_BYTES = 18
METROID_ALPHABET = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz?-'

#def passwordStringToMetroidAlphabet(passwordString):
#  passwordBytes = []

#  currentChar = passwordString[i]

def bufferToPasswordCharacters(buffer):
  password = []
  for i in range(int(PASSWORD_SIZE_BYTES * 8 / 6)):
#    print(i * 6) 
    bit = i * 6
#    print(f"chunk {i} bits {bit}: {buffer.getBits(bit, 6):06b}")
    

    byte = buffer.getBits(bit, 6)
#    byte = buffer.getChunk(bit)
    print(f"bit: {bit} chunk: {byte}")

    bit0 = (byte >> 0) & 1 
    bit1 = (byte >> 1) & 1 
    bit2 = (byte >> 2) & 1 
    bit3 = (byte >> 3) & 1 
    bit4 = (byte >> 4) & 1 
    bit5 = (byte >> 5) & 1 
 
    bits = [bit0, bit1, bit2, bit3, bit4, bit5]
    print(bits)

    reverse_chunk = 0x0
    if bit0:
      reverse_chunk |= (1 << 5)
    if bit1:
      reverse_chunk |= (1 << 4)
    if bit2:
      reverse_chunk |= (1 << 3)
    if bit3:
      reverse_chunk |= (1 << 2)
    if bit4:
      reverse_chunk |= (1 << 1)
    if bit5:
      reverse_chunk |= (1 << 0)

    print(f"reverse_chunk {i}: {reverse_chunk:06b}")
    
#    c = METROID_ALPHABET[int(byte)] 
    c = METROID_ALPHABET[int(reverse_chunk)] 
    print(f"= {c}")

  for i in range(int(PASSWORD_SIZE_BYTES * 8)):
    print(f"{i}: {buffer.getBit(i)}")
#    iChunk = 0
#    for i in range(8):
#      print(f"{bit + i}: {buffer.getBit(bit + i)}")
#      iChunk += buffer.getBit(bit + i) * (2 ** (5 - i)) 
#    print(f"iChunk: {iChunk}")

    
    password.append(c)

  str_password = "".join(password)
#  print(f"password = {str_password}")
  print("password")
  print(f"{str_password[0:6]} {str_password[6:12]}")
  print(f"{str_password[12:18]} {str_password[18:24]}")


def setChecksum(bytes):
  CHECKSUM_BYTE = 17
  checksum_i = 0
  for i in range(CHECKSUM_BYTE):
    checksum_i += bytes[i]
  print(f"checksum: {checksum_i}")
  bytes[CHECKSUM_BYTE] = checksum_i & 0xFF

#buffer = BitBuffer()

#MARU_MARI_BIT = 76
#buffer.setBit(MARU_MARI_BIT, True)

#LONG_BEAM_BIT = 74
#buffer.setBit(LONG_BEAM_BIT, True)

#buffer.setByte(MISSILE_BYTE, 3)
#buffer.setByte(MISSILE_BYTE, 255)
#buffer.setBit(MISSILE_1_BIT, True)


#buffer.setBit(8, True)
#buffer.setBit(9, True)
#buffer.setBit(10, True)
#buffer.setBit(9, False)

#buffer.setBit(64, True)


#print("checksum: " + str(buffer.calcChecksum()))
#CHECKSUM_INDEX = 136
#buffer.setBits(CHECKSUM_INDEX, buffer.calcChecksum())
#buffer.setByte(17, 1)

#buffer.setByte(17, 3)
#buffer.setByte(17, 255)
#buffer.setByte(17, 254)

#buffer.printBuffer()

#print(f"getBits(72, 6):  {buffer.getBits(72, 6):06b}")


#buffer.setBit(7, True)
#buffer.setBit(8, True)
#buffer.printBuffer()
#print("checksum: " + str(buffer.calcChecksum()))
#print(f"getBits(6, 6):  {buffer.getBits(6, 6):06b}")

#print(f"7: {buffer.getBit(7)}")
#print(f"8: {buffer.getBit(8)}")
#print(f"9: {buffer.getBit(9)}")
#print(f"72: {buffer.getBit(72)}")
#print(f"76: {buffer.getBit(76)}")


#bufferToPasswordCharacters(buffer)

#print(f"last byte: {buffer.getByte(17)}")


#byte = 0x0F
#chunk = int(byte)
#print(f"chunk: {chunk:06b}")
#print(f"mt char: {METROID_ALPHABET[chunk]}")

#byte = 0x3C
#chunk = int(byte)
#print(f"chunk: {chunk:06b}")
#print(f"mt char: {METROID_ALPHABET[chunk]}")

#print(f"mt char 60: {METROID_ALPHABET[60]}")
#print(f"mt char 0: {METROID_ALPHABET[0]}")
#print(f"mt char 10: {METROID_ALPHABET[10]}")
#print(f"mt char 36: {METROID_ALPHABET[36]}")

#print(f"{buffer.data[17]}")

def bytesToMetroidAlphabet(bytes):
#  checksum = bytearray(6) 
#  checksum[0] = 0b00000000
#  checksum[1] = 0b00000000
#  checksum[2] = 0b11000001
#  checksum[3] = 0b10000111
#  checksum[4] = 0b11110000
#  checksum[5] = 0b01000001 
  checksum = bytes

#  part2 = (checksum[2] & 0b00111111)
#  chunk20 = (part2 >> 0)
#  print(f"chunk20: {METROID_ALPHABET[chunk20]}")

  part2 = (checksum[0] & 0b11111100)
  chunk0 = (part2 >> 2)
#  print(f"chunk0: {METROID_ALPHABET[chunk0]}")

  part1 = (checksum[0] & 0b00000011)
  part2 = (checksum[1] & 0b11110000)
  chunk1 = (part1 << 4) | (part2 >> 4)
#  print(f"chunk1: {METROID_ALPHABET[chunk1]}")

  part1 = (checksum[1] & 0b00001111)
  part2 = (checksum[2] & 0b11000000)
  chunk2 = (part1 << 2) | (part2 >> 6)
#  print(f"chunk2: {METROID_ALPHABET[chunk2]}")

  chunk3 = checksum[2] & 0b00111111
#  print(f"chunk3: {METROID_ALPHABET[chunk3]}")

  print(f"{METROID_ALPHABET[chunk0]}{METROID_ALPHABET[chunk1]}{METROID_ALPHABET[chunk2]}{METROID_ALPHABET[chunk3]}")

#MISSILE_BYTE = 10
#buffer.setByte(MISSILE_BYTE, 8)

#CHECKSUM_BYTE = 17
#buffer.setByte(17, 8)

#print("8 missiles")
#bytesToMetroidAlphabet([0x00, 0x00, 0x00])
#bytesToMetroidAlphabet([0x00, 0x00, 0x00])
#bytesToMetroidAlphabet([0x00, 0x00, 0x00])
#print("")
#bytesToMetroidAlphabet([0x00, 0x00, 0x00])
#bytesToMetroidAlphabet([0x00, 0x08, 0x00])
#bytesToMetroidAlphabet([0x00, 0x00, 0x08])

#print("Start in Norfair")
#bytesToMetroidAlphabet([0x00, 0x00, 0x00])
#bytesToMetroidAlphabet([0x00, 0x00, 0x00])
#bytesToMetroidAlphabet([0x00, 0x00, 0x01])
#print("")
#bytesToMetroidAlphabet([0x00, 0x00, 0x00])
#bytesToMetroidAlphabet([0x00, 0x00, 0x00])
#bytesToMetroidAlphabet([0x00, 0x00, 0x01])


#print("Ice Beam (byte 9 bit 7) with 5 missile (byte 10) ")
#bytes = [0x00, 0x00, 0x00,
#         0x00, 0x00, 0x00,
#         0x00, 0x00, 0x00,
#         0x80, 0x05, 0x00,
#         0x00, 0x00, 0x00,
#         0x00, 0x00, 0x00
#        ]
#setChecksum(bytes)

#for i in range(6):
#  bytesToMetroidAlphabet([bytes[(3 * i)], bytes[(3 * i) + 1], bytes[(3 * i) + 2]])

#print("Ice Beam (byte 9 bit 7) with 5 missile (byte 10) ")
bytes = [0x00, 0x00, 0x00,
         0x00, 0x00, 0x00,
         0x00, 0x00, 0x00,
         0x00, 0x00, 0x00,
         0x00, 0x00, 0x00,
         0x00, 0x00, 0x00
        ]

MISSILE_BYTE = 10
bytes[MISSILE_BYTE] = 0x19 

POWERUP_BYTE = 9 
POWERUP_BOMBS_BIT = 0b00000001
POWERUP_HIGHJUMP_BIT = 0b00000010
POWERUP_SCREWATTACK_BIT = 0b00001000
POWERUP_SCREWATTACK_BIT = 0b00001000
POWERUP_ICEBEAM_BIT = 0b10000000
bytes[POWERUP_BYTE] = POWERUP_ICEBEAM_BIT | POWERUP_SCREWATTACK_BIT | POWERUP_HIGHJUMP_BIT

setChecksum(bytes)

for i in range(6):
  bytesToMetroidAlphabet([bytes[(3 * i)], bytes[(3 * i) + 1], bytes[(3 * i) + 2]])

