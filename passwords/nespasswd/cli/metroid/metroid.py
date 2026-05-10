# 2026 Levi D. Smith <developer@levidsmith.com>

PASSWORD_SIZE_BYTES = 18
METROID_ALPHABET = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz?-'

def setChecksum(bytes):
  CHECKSUM_BYTE = 17
  checksum_i = 0
  for i in range(CHECKSUM_BYTE):
    checksum_i += bytes[i]
#  print(f"checksum: {checksum_i}")
  bytes[CHECKSUM_BYTE] = checksum_i & 0xFF

def bytesToMetroidAlphabet(bytes):
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

#  print("".join(METROID_ALPHABET[x] for x in chars))
  return "".join(METROID_ALPHABET[x] for x in chars)

bytes = [0x00] * PASSWORD_SIZE_BYTES

MISSILE_BYTE = 10
bytes[MISSILE_BYTE] = 0xFF  # 255 missiles 

POWERUP_BYTE = 9 
POWERUP_BOMBS_BIT = 0b00000001
POWERUP_HIGHJUMP_BIT = 0b00000010
POWERUP_LONGBEAM_BIT = 0b00000100
POWERUP_SCREWATTACK_BIT = 0b00001000
POWERUP_MARUMARI_BIT = 0b00010000
POWERUP_VARIA_BIT = 0b00100000
POWERUP_WAVBEAM_BIT = 0b01000000
POWERUP_ICEBEAM_BIT = 0b10000000
bytes[POWERUP_BYTE] = POWERUP_ICEBEAM_BIT | POWERUP_SCREWATTACK_BIT | POWERUP_HIGHJUMP_BIT | POWERUP_LONGBEAM_BIT | POWERUP_MARUMARI_BIT | POWERUP_VARIA_BIT | POWERUP_BOMBS_BIT

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

for d in DOORS:
  bytes[d["byte"]] |= d["bit"]

for tank in ENERGY_TANKS:
  bytes[tank["byte"]] |= tank["bit"]

for mc in MISSILE_CONTAINERS:
  bytes[mc["byte"]] |= mc["bit"]

for zd in ZEBETITE_DEFEATED:
  bytes[zd["byte"]] |= zd["bit"]

bytes[STATUS["kraid_statue"]["byte"]] |= STATUS["kraid_statue"]["bit"]
bytes[STATUS["kraid_defeated"]["byte"]] |= STATUS["kraid_defeated"]["bit"]
bytes[STATUS["ridley_statue"]["byte"]] |= STATUS["ridley_statue"]["bit"]
bytes[STATUS["ridley_defeated"]["byte"]] |= STATUS["ridley_defeated"]["bit"]

setChecksum(bytes)

password = ""
for i in range(6):
  password += bytesToMetroidAlphabet([bytes[(3 * i)], bytes[(3 * i) + 1], bytes[(3 * i) + 2]])

for i in range(4):
  print(password[i * 6:(i + 1) * 6], end=" ")
  if i % 2 == 1:
    print("")
