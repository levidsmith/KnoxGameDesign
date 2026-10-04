#2026 Levi D. Smith <developer@levidsmith.com>
#takes a string as input, and generates a 6502 hex list 
#compatible with NES pattern table
str = input("Enter string\n")
str = str.upper()
print(str.upper())

pad_length=20
str = str.ljust(pad_length, ' ')

iSpaceTile=36

listValues = []
for c in str:


    if c == ' ':
#        listValues.append('$24')
        listValues.append('$' + format(iSpaceTile, '02x'))     
    elif ord(c) >= ord('0') and ord(c) <= ord('9'):
        iTile = ord(c) - ord('0')
        listValues.append('$' + format(iTile, '02x'))     
    elif ord(c) >= ord('A') and ord(c) <= ord('Z'):          
        iTile = ord(c) - ord('A') + 10
        listValues.append('$' + format(iTile, '02x'))     
    else:
        listValues.append('$' + format(iSpaceTile, '02x'))     
#    print(listValues)

print(', '.join(listValues))
