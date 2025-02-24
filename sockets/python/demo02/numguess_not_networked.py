import random

iMin = 1
iMax = 100
iSecretNum = random.randrange(iMin, iMax + 1)

iGuess = -1
iGuessCount = 0

while (iGuess != iSecretNum):
    iGuess = int(input())
    iGuessCount += 1

    if (iGuess < iSecretNum):
        print("Higher")
    elif (iGuess > iSecretNum):
        print("Lower")
    elif (iGuess == iSecretNum):
        print("Correct! ", iGuessCount, " total guesses")

