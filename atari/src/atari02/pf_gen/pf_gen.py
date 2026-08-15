#2026 Levi D. Smith for Knox Game Design - August 2026
f = open("knox.txt")

data  = f.readlines()
int_data = []


for i in range(len(data)):
  if i == 0:
    continue
  val = data[i].rstrip()
  val = val.replace('#', '1')
  val = val.replace(' ', '0')
  val = val.ljust(8, '0') 
  val = int(val, 2)
  int_data.append(val)

while(len(int_data) < 192):
  int_data.append(0)

for i in range(6):
  print (f"knox_STRIP_{i}")
  for j in range(192):
#    if True:        
    if i == 1:        
#      print(f" .byte {int_data[j]}")
      print(f" .byte {int_data[j // 3]}")
    else:
      print(" .byte 0")
