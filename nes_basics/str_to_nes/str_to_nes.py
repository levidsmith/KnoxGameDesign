#2026 Levi D. Smith <developer@levidsmith.com>
#takes a string as input, and generates a 6502 hex list 
#compatible with NES pattern table
str = input("Enter string\n")
str = str.upper()
print(str.upper())

listValues = []
for c in str:

    if c == ' ':
        listValues.append('$24')
    else:          
        i = ord(c) - ord('A') + 10
        listValues.append('$' + format(i, '02x'))     
#    print(listValues)

print(','.join(listValues))
