# 2026 Levi D. Smith <developer@levidsmith.com>
from pydantic import BaseModel

PASSWORD_SIZE_BYTES = 18

class QueryParams(BaseModel):
  bombs: bool | None = False
  highJump: bool | None = False
  longBeam: bool | None = False
  screwAttack: bool | None = False
  maruMari: bool | None = False
  varia: bool | None = False
  waveBeam: bool | None = False
  iceBeam: bool | None = False

  swimsuit: bool | None = False
  startRidley: bool | None = False
  startKraid: bool | None = False
  startNorfair: bool | None = False

  ridley: bool | None = False
  kraid: bool | None = False

  missiles: int | None = 0
  missileContainers: int | None = 0
  energyTanks: int | None = 0 

def get_password(query: QueryParams):
  bytes = [0x00] * PASSWORD_SIZE_BYTES


  if query.missiles is not None:
    MISSILE_BYTE = 10
    bytes[MISSILE_BYTE] = int(query.missiles)

  POWERUP_BYTE = 9 
  POWERUP_BOMBS_BIT = 0b00000001
  POWERUP_HIGHJUMP_BIT = 0b00000010
  POWERUP_LONGBEAM_BIT = 0b00000100
  POWERUP_SCREWATTACK_BIT = 0b00001000
  POWERUP_MARUMARI_BIT = 0b00010000
  POWERUP_VARIA_BIT = 0b00100000
  POWERUP_WAVEBEAM_BIT = 0b01000000
  POWERUP_ICEBEAM_BIT = 0b10000000

  if query.bombs:
    bytes[POWERUP_BYTE] |= POWERUP_BOMBS_BIT

  if query.highJump:
    bytes[POWERUP_BYTE] |= POWERUP_HIGHJUMP_BIT

  if query.longBeam:
    bytes[POWERUP_BYTE] |= POWERUP_LONGBEAM_BIT

  if query.screwAttack:
    bytes[POWERUP_BYTE] |= POWERUP_SCREWATTACK_BIT

  if query.maruMari:
    bytes[POWERUP_BYTE] |= POWERUP_MARUMARI_BIT

  if query.varia:
    bytes[POWERUP_BYTE] |= POWERUP_VARIA_BIT

  if query.waveBeam:
    bytes[POWERUP_BYTE] |= POWERUP_WAVEBEAM_BIT

  if query.iceBeam:
    bytes[POWERUP_BYTE] |= POWERUP_ICEBEAM_BIT

  STARTLOCATION_BYTE = 8 
  SWIMSUIT_BIT = 0b10000000
  STARTRIDLEY_BIT = 0b00000100
  STARTKRAID_BIT = 0b00000010
  STARTNORFAIR_BIT = 0b00000001

  if query.swimsuit:
    bytes[STARTLOCATION_BYTE] |= SWIMSUIT_BIT

  if query.startRidley:
    bytes[STARTLOCATION_BYTE] |= STARTRIDLEY_BIT

  if query.startKraid:
    bytes[STARTLOCATION_BYTE] |= STARTKRAID_BIT

  if query.startNorfair:
    bytes[STARTLOCATION_BYTE] |= STARTNORFAIR_BIT

  DOORS = [
          { "byte": 0, "bit": 0b10000000 },
          { "byte": 0, "bit": 0b00000000 },
          { "byte": 0, "bit": 0b00010000 },
          { "byte": 0, "bit": 0b00000100 },
          { "byte": 1, "bit": 0b10000000 },
          { "byte": 1, "bit": 0b00000100 },
          { "byte": 2, "bit": 0b10000000 },
          { "byte": 3, "bit": 0b00100000 },
          { "byte": 3, "bit": 0b00000010 },
          { "byte": 4, "bit": 0b01000000 },
          { "byte": 4, "bit": 0b00100000 },
          { "byte": 4, "bit": 0b00001000 },
          { "byte": 4, "bit": 0b00000001 },
          { "byte": 5, "bit": 0b10000000 },
          { "byte": 5, "bit": 0b00010000 },
          { "byte": 5, "bit": 0b00000010 },
          { "byte": 6, "bit": 0b00010000 },
          { "byte": 6, "bit": 0b00001000 },
          { "byte": 6, "bit": 0b00000100 },
          ]

  ENERGY_TANKS = [
                 { "byte": 0, "bit": 0b00010000 },
                 { "byte": 1, "bit": 0b00010000 },
                 { "byte": 1, "bit": 0b00000010 },
                 { "byte": 3, "bit": 0b01000000 },
                 { "byte": 4, "bit": 0b00010000 },
                 { "byte": 5, "bit": 0b00100000 },
                 { "byte": 5, "bit": 0b00000100 },
                 { "byte": 6, "bit": 0b00000001 },
                 ]

  ZEBETITE_DEFEATED = [
                      { "byte": 6, "bit": 0b10000000 },
                      { "byte": 6, "bit": 0b01000000 },
                      { "byte": 6, "bit": 0b00100000 },
                      { "byte": 7, "bit": 0b00000010 },
                      { "byte": 7, "bit": 0b00000001 },
                      ]

  MISSILE_CONTAINERS = [
                       { "byte": 0, "bit": 0b00000010 },
                       { "byte": 1, "bit": 0b01000000 },
                       { "byte": 1, "bit": 0b00100000 },
                       { "byte": 1, "bit": 0b00000001 },
                       { "byte": 2, "bit": 0b01000000 },
                       { "byte": 2, "bit": 0b00100000 },
                       { "byte": 2, "bit": 0b00010000 },
                       { "byte": 2, "bit": 0b00001000 },
                       { "byte": 2, "bit": 0b00000100 },
                       { "byte": 2, "bit": 0b00000010 },
                       { "byte": 2, "bit": 0b00000001 },
                       { "byte": 3, "bit": 0b10000000 },
                       { "byte": 3, "bit": 0b00010000 },
                       { "byte": 3, "bit": 0b00001000 },
                       { "byte": 4, "bit": 0b10000000 },
                       { "byte": 4, "bit": 0b00000100 },
                       { "byte": 4, "bit": 0b00000010 },
                       { "byte": 5, "bit": 0b01000000 },
                       { "byte": 5, "bit": 0b00001000 },
                       { "byte": 5, "bit": 0b00000001 },
                       { "byte": 6, "bit": 0b00000010 },
                       ]
  STATUS = {
           "kraid_statue":     { "byte": 15, "bit": 0b10000000 },
           "kraid_defeated":   { "byte": 15, "bit": 0b01000000 },
           "ridley_statue":    { "byte": 15, "bit": 0b00100000 },
           "ridley_defeated":  { "byte": 15, "bit": 0b00010000 },
           "samus_swimsuit":   { "byte":  8, "bit": 0b10000000 },
         }

#  for d in DOORS:
#    bytes[d["byte"]] |= d["bit"]

#  for tank in ENERGY_TANKS:
#    bytes[tank["byte"]] |= tank["bit"]

#  for mc in MISSILE_CONTAINERS:
#    bytes[mc["byte"]] |= mc["bit"]

#  for zd in ZEBETITE_DEFEATED:
#    bytes[zd["byte"]] |= zd["bit"]

  if query.kraid:
    bytes[STATUS["kraid_statue"]["byte"]] |= STATUS["kraid_statue"]["bit"]
    bytes[STATUS["kraid_defeated"]["byte"]] |= STATUS["kraid_defeated"]["bit"]
  if query.ridley:
    bytes[STATUS["ridley_statue"]["byte"]] |= STATUS["ridley_statue"]["bit"]
    bytes[STATUS["ridley_defeated"]["byte"]] |= STATUS["ridley_defeated"]["bit"]

  for i in range(query.energyTanks):   
    tank = ENERGY_TANKS[i]
    bytes[tank["byte"]] |= tank["bit"]

#  for i in range(int(query.missiles / 5)):
  for i in range(int(query.missileContainers)):
    mc = MISSILE_CONTAINERS[i]
    bytes[mc["byte"]] |= mc["bit"]

  setChecksum(bytes)

  password = ""
  for i in range(6):
    password += bytesToMetroidAlphabet([bytes[(3 * i)], bytes[(3 * i) + 1], bytes[(3 * i) + 2]])

  password_formatted = ""
  for i in range(4):
    password_formatted += password[i * 6:(i + 1) * 6]
    if (i != 3):
      password_formatted += " " 
  return password_formatted

def setChecksum(bytes):
  CHECKSUM_BYTE = 17
  checksum_i = 0
  for i in range(CHECKSUM_BYTE):
    checksum_i += bytes[i]
  bytes[CHECKSUM_BYTE] = checksum_i & 0xFF

def bytesToMetroidAlphabet(bytes):
  METROID_ALPHABET = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz?-'
  chars = ['', '', '', ''] 
 
  part1 = (bytes[0] & 0b11111100)
  chars[0] = (part1 >> 2)

  part1 = (bytes[0] & 0b00000011)
  part2 = (bytes[1] & 0b11110000)
  chars[1] = (part1 << 4) | (part2 >> 4)

  part1 = (bytes[1] & 0b00001111)
  part2 = (bytes[2] & 0b11000000)
  chars[2] = (part1 << 2) | (part2 >> 6)

  chars[3] = bytes[2] & 0b00111111

  return "".join(METROID_ALPHABET[x] for x in chars)

