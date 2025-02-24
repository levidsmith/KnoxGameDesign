import socket

s = socket.socket()
port = 37830
s.bind(('', port))
s.listen(5)
c, addr = s.accept()
print("Socket running: ", addr)
while True:
    rcvdData = c.recv(1024).decode()
    print("SERVER RECEIVE: ", rcvdData)
    sendData = input("SERVER SEND: ")
    c.send(sendData.encode())
    if (sendData == "quit"):
        break
c.close()
