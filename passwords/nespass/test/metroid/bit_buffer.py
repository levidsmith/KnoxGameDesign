# 2026 Levi D. Smith <developer@levidsmith.com>

import math

PASSWORD_SIZE_BYTES = 18

class BitBuffer:
  def __init__(self):
    self.data = bytearray(PASSWORD_SIZE_BYTES)

  def setBit(self, bit, value):
    byte_index = math.floor(bit / 8)
    if (value):
      self.data[byte_index] |= (1 << (bit % 8))
    else:
      self.data[byte_index] &= ~(1 << (bit % 8))

  def setBits(self, bit, bits): 
    byte_index = math.floor(bit / 8) 
#    self.data[byte_index] = bits 
    self.data[byte_index] = 0x80 

  def setByte(self, byte_index, value):
    self.data[byte_index] = value

  def printBuffer(self):
    i = 0
    for byte in self.data:
      endChar = " "  
      if (i % 2 == 1):
        endChar = "\n"
      str = f"{byte:08b}"
#      str = f"{byte}"
#      print(str[::-1], end=endChar)
      print(str, end=endChar)
      i += 1
    print()

    print(f"print bytearray: {self.data}")

  def calcChecksum(self):
    checksum = 0
    for byte in self.data:
      checksum += byte.bit_count()

    return checksum

  def getBit(self, bit):
    byte_index = math.floor(bit / 8)
    if self.data[byte_index] & (1 << (bit % 8)):
      return True
    else:
      return False

  def getBits(self, bit, count):
    byte = 0x00 
    for i in range(count):
      if self.getBit(bit + i):
        byte |= (1 << (i % 8))
      
    return byte

#  def getChunk(self, bit):
#    integer_value = int.from_bytes(data, byteorder='big')
#    shifted = integer_value << bit 

  def getByte(self, byte_index):
    return self.data[byte_index]
