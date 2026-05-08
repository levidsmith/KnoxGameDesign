# 2026 Levi D. Smith <developer@levidsmith.com>
from pydantic import BaseModel

class QueryParams(BaseModel):
  hasBombs: bool | None = False
  hasHighJumpBoots: bool | None = False
  hasLongBeam: bool | None = False
  hasScrewAttack: bool | None = False
  hasMaruMari: bool | None = False
  hasVaria: bool | None = False
  hasWaveBeam: bool | None = False
  hasIceBeam: bool | None = False

  swimsuitEnabled: bool | None = False

  ridleyDefeated: bool | None = False
  kraidDefeated: bool | None = False

def get_password(query: QueryParams):
#  data = [0x41, 0x42, 0x43]
  data = [0b00000001, 0b00000000, 0b00000000, 0b00000000,
          0b00000000, 0b00000000, 0b00000000, 0b00000000,
          0b00000000, 0b01111111, 0b00000000, 0b00000000,
          0b00000000, 0b00000000, 0b00000000, 0b00000000,
          0b00000000, 0b00000000
         ]
#  data[9] = 0b01111111
#  data[1] = 0b01000001
 
  c = 0b00000000

  bit_count = 0
  for i in range(0, 17):
    bit_count += data[i].bit_count()

#  bit_count = data[1].bit_count()

#  password = "".join(map(chr, data)) + "bit count: " + str(bit_count)
  password = "".join(map(convertToMetroidChar, data)) + "bit count: " + str(bit_count)

#  if query.hasMaruMari:
#    password += "MM"

  data = { "password": password }
  return data

def convertToMetroidChar(c):
  if c < 10:
#    return c | 0b00110000 
    return '0'
  else:
    return 'A'
