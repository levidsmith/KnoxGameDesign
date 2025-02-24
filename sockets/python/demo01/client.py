import socket

s = socket.socket()
s.connect(('127.0.0.1', 37830))
while True:
    str = input("CLIENT SEND: ")
    s.send(str.encode());
    if (str == "quit"):
        break
    print("CLIENT RECEIVE:", s.recv(1024).decode())
s.close()
