import socket
import random

s = socket.socket()
port = 37830
s.bind(('', port))
s.listen(5)
c, addr = s.accept()

iMin = 1
iMax = 100
iSecretNum = random.randrange(iMin, iMax + 1)
iGuessCount = 0

print("Socket running: ", addr)
while True:
    rcvdData = c.recv(1024).decode()
    print("SERVER RECEIVE: ", rcvdData)

    if (rcvdData == "quit"):
        break

    iGuess = int(rcvdData)
    iGuessCount += 1

    if (iGuess < iSecretNum):
        sendData = "Higher"
    elif (iGuess > iSecretNum):
        sendData = "Lower"
    elif(iGuess == iSecretNum):
        sendData = "Correct " + str(iGuessCount) + " total guesses"

    c.send(sendData.encode())
c.close()
