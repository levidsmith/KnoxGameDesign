# 2026 Levi D. Smith <developer@levidsmith.com>
# based on https://github.com/alexras/truepeacein.space
# by alexras

import math

class BitBuffer:
  def __init__(self):
    self.data = bytearray(18)

  def setBit(self, bit, value):
    byte_index = math.floor(bit / 8)
    if (value):
      self.data[byte_index] |= (1 << (bit % 8))
    else:
      self.data[byte_index] &= ~(1 << (bit % 8))

  def printBuffer(self):
    i = 0
    for byte in self.data:
      endChar = " "  
      if (i % 4 == 3):
        endChar = "\n"
      str = f"{byte:08b}"
      print(str[::-1], end=endChar)
      i += 1
    print()

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
