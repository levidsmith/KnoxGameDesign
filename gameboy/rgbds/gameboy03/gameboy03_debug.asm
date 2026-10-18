; original Hello World source from https://gbdev.io/gb-asm-tutorial/assets/hello-world.asm 
; Modifications for Knox Game Design by Levi D. Smith <developer@levidsmith.com>
; 2026-10-04
INCLUDE "hardware.inc"

SECTION "Header", ROM0[$100]

	jp EntryPoint

	ds $150 - @, 0 ; Make room for the header

EntryPoint:
	; Shut down audio circuitry
	ld a, 0
	ld [rNR52], a

        ;Initialize variables
        ld a, 0
        ld [wCounterDigitOnes], a
        ld [wCounterDigitTens], a

        ld a, 0
        ld [wSecretNumDigitOnes], a
        ld a, 0
        ld [wSecretNumDigitTens], a

        ld a, 0
        ld [wGuessNumDigitOnes], a
        ld a, 0
        ld [wGuessNumDigitTens], a

        ld a, 0
        ld [wGuessCountDigitOnes], a
        ld a, 0
        ld [wGuessCountDigitTens], a
  
        ld a, $FF
        ld [wResult], a


        ld [wCurKeys], a
        ld [wNewKeys], a


WaitVBlank:
	ld a, [rLY]
	cp 144
	jr c, WaitVBlank

	; Turn the LCD off
	ld a, 0
	ld [rLCDC], a

	; Copy the tile data
	ld de, Tiles
	ld hl, $9000
	ld bc, Tiles.End - Tiles
CopyTiles:
	ld a, [de]
	ld [hli], a
	inc de
	dec bc
	ld a, b
	or a, c
	jr nz, CopyTiles

	; Copy the tilemap
	ld de, Tilemap
	ld hl, $9800
	ld bc, Tilemap.End - Tilemap
CopyTilemap:
	ld a, [de]
	ld [hli], a
	inc de
	dec bc
	ld a, b
	or a, c
	jr nz, CopyTilemap

	ld a, $0a
	ld [hli], a

	; Turn the LCD on
	ld a, LCDC_ON | LCDC_BG_ON
	ld [rLCDC], a

	; During the first (blank) frame, initialize display registers
	ld a, %11100100
	ld [rBGP], a

    ; Initialize variables
	ld a, 0
	ld [wCounterDigitOnes], a
	ld [wCounterDigitTens], a

	call IncrementCounter	

Done:
	ld a, [rLY]
	cp 144
	jr c, NotInVBlank

        call UpdateKeys

CheckLeft:
        ld a, [wNewKeys]
        and a, PAD_LEFT
        jp z, CheckRight
Left:
        call GuessSubOne
        
CheckRight:
        ld a, [wNewKeys]
        and a, PAD_RIGHT
        jp z, CheckUp
Right:
        call GuessAddOne

CheckUp:
        ld a, [wNewKeys]
        and a, PAD_UP
        jp z, CheckDown
Up:
        call GuessAddTen

CheckDown:
        ld a, [wNewKeys]
        and a, PAD_DOWN
        jp z, CheckButtonA
Down:
        call GuessSubTen

CheckButtonA:
        ld a, [wNewKeys]
        and a, PAD_A
        jp z, CheckStart
ButtonA:
        call CheckGuess

CheckStart:
        ld a, [wNewKeys]
        and a, PAD_START
        jp z, InputDone
Start:
        call SetSecret

InputDone:

	call IncrementCounter
        call DisplayText 

NotInVBlank:
	jr Done

IncrementCounter:		
	ld a, [wCounterDigitOnes]
	inc a
	ld [wCounterDigitOnes], a

	ld a, [wCounterDigitOnes]
	cp a, $A                  ; compare ones digit with 10 ($A)
	jp nz, CounterResetTensDone       ; reset ones digit to 0 if equal to 10.  otherwise do jump to end of reset code

CounterResetOnes:
    ld a, 0
	ld [wCounterDigitOnes], a

    ld a, [wCounterDigitTens]
	inc a
	ld [wCounterDigitTens], a

	cp a, $A   ; compare tens digit with 10 ($A)
	jp nz, CounterResetTensDone

CounterResetOnesDone:

CounterResetTens:
	ld a, 0
	ld [wCounterDigitTens], a
CounterResetTensDone:
    ret

DisplayText:
        ;Display new counter digits	
	ld hl, $9842
	ld a, [wCounterDigitTens]
	ld [hli], a
	ld a, [wCounterDigitOnes]
	ld [hl], a

        ;Display secret number
	ld hl, $98A2
	ld a, [wSecretNumDigitTens]
	ld [hli], a
	ld a, [wSecretNumDigitOnes]
	ld [hl], a

        ;Display guess count
	ld hl, $9902
	ld a, [wGuessCountDigitTens] 
	ld [hli], a
	ld a, [wGuessCountDigitOnes] 
	ld [hl], a

        ;Display guessed number
	ld hl, $9962
	ld a, [wGuessNumDigitTens]
	ld [hli], a
	ld a, [wGuessNumDigitOnes]
	ld [hl], a

        ;Display result
        ld a, [wResult]
        cp a, 0
        jp z, DisplayResultCorrect
        cp a, 1
        jp z, DisplayResultHigher
        cp a, 2
        jp z, DisplayResultLower

DisplayResultCorrect:
        ld hl, $99A2
        ld bc, StrCorrect 
        call MemCopy

        jp DisplayResultDone
DisplayResultHigher:
        ld hl, $99A2
        ld bc, StrLower ;if the guess is lower, display higher to the player 
        call MemCopy

        jp DisplayResultDone
DisplayResultLower:
        ld hl, $99A2
        ld bc, StrHigher ;if the guess is higher, display lower to the player 
        call MemCopy

        jp DisplayResultDone
DisplayResultDone:


DisplayTextDone:
	ret

MemCopy: 
        ;Copy 8 bytes at address bc to address at hl
        ld a, [bc] 
        ld [hl], a
        inc hl
        inc bc

        ld a, [bc] 
        ld [hl], a
        inc hl
        inc bc

        ld a, [bc] 
        ld [hl], a
        inc hl
        inc bc

        ld a, [bc] 
        ld [hl], a
        inc hl
        inc bc

        ld a, [bc] 
        ld [hl], a
        inc hl
        inc bc

        ld a, [bc] 
        ld [hl], a
        inc hl
        inc bc

        ld a, [bc] 
        ld [hl], a
        inc hl
        inc bc

        ld a, [bc] 
        ld [hl], a
        inc hl
        inc bc


        ret

GuessAddOne:
        ld a, [wGuessNumDigitOnes]
        inc a
        ld [wGuessNumDigitOnes], a

        cp a, $A
        jp nz, GuessAddOneDone

        ld a, 0
        ld [wGuessNumDigitOnes], a
GuessAddOneDone:
        ret

GuessSubOne:
        ld a, [wGuessNumDigitOnes]
        dec a
        ld [wGuessNumDigitOnes], a

        cp a, $FF
        jp nz, GuessSubOneDone

        ld a, 9
        ld [wGuessNumDigitOnes], a
GuessSubOneDone:
        ret


GuessAddTen:
        ld a, [wGuessNumDigitTens]
        inc a
        ld [wGuessNumDigitTens], a

        cp a, $A
        jp nz, GuessAddTenDone

        ld a, 0
        ld [wGuessNumDigitTens], a
GuessAddTenDone:
        ret

GuessSubTen:
        ld a, [wGuessNumDigitTens]
        dec a
        ld [wGuessNumDigitTens], a

        cp a, $FF
        jp nz, GuessSubTenDone

        ld a, 9
        ld [wGuessNumDigitTens], a
GuessSubTenDone:
        ret



CheckGuess:
        ;increment the guess count
        ld a, [wGuessCountDigitOnes]
        inc a 
        ld [wGuessCountDigitOnes], a 
        cp a, $A
        jp nz, GuessCountIncrementDone
          
        ld a, 0
        ld [wGuessCountDigitOnes], a

        ld a, [wGuessCountDigitTens]
        inc a
        ld [wGuessCountDigitTens], a
GuessCountIncrementDone:

        ;store guess result
        ld hl, wSecretNumDigitTens
        ld a, [wGuessNumDigitTens]
        cp a, [hl]
        jp z, CheckGuessTensCorrect
        jp c, CheckGuessTensLower
        jp CheckGuessTensHigher

CheckGuessTensCorrect:
        jp CheckGuessOnes
CheckGuessTensHigher:
        ld a, 1
        ld [wResult], a
        jp CheckGuessDone
CheckGuessTensLower:
        ld a, 2
        ld [wResult], a
        jp CheckGuessDone

CheckGuessOnes:
        ld hl, wSecretNumDigitOnes
        ld a, [wGuessNumDigitOnes]
        cp a, [hl]
        jp z, CheckGuessOnesCorrect
        jp c, CheckGuessOnesLower
        jp CheckGuessOnesHigher

CheckGuessOnesCorrect:
        ld a, 0
        ld [wResult], a
        jp CheckGuessDone
CheckGuessOnesHigher:
        ld a, 1
        ld [wResult], a
        jp CheckGuessDone
CheckGuessOnesLower:
        ld a, 2
        ld [wResult], a
        jp CheckGuessDone



CheckGuessDone:

        ;clear the current guess
        ld a, 0
        ld [wGuessNumDigitOnes], a
        ld [wGuessNumDigitTens], a
        

        ret

SetSecret:
        ld a, [wCounterDigitOnes]
        ld [wSecretNumDigitOnes], a

        ld a, [wCounterDigitTens]
        ld [wSecretNumDigitTens], a

        ld a, 0
        ld [wGuessCountDigitOnes], a
        ld [wGuessCountDigitTens], a

        ret

UpdateKeys:
  ; Poll half the controller
  ld a, JOYP_GET_BUTTONS
  call .onenibble
  ld b, a ; B7-4 = 1; B3-0 = unpressed buttons

  ; Poll the other half
  ld a, JOYP_GET_CTRL_PAD
  call .onenibble
  swap a ; A7-4 = unpressed directions; A3-0 = 1
  xor a, b ; A = pressed buttons + directions
  ld b, a ; B = pressed buttons + directions

  ; And release the controller
  ld a, JOYP_GET_NONE
  ldh [rJOYP], a

  ; Combine with previous wCurKeys to make wNewKeys
  ld a, [wCurKeys]
  xor a, b ; A = keys that changed state
  and a, b ; A = keys that changed to pressed
  ld [wNewKeys], a
  ld a, b
  ld [wCurKeys], a
  ret

.onenibble
  ldh [rJOYP], a ; switch the key matrix
  call .knownret ; burn 10 cycles calling a known ret
  ldh a, [rJOYP] ; ignore value while waiting for the key matrix to settle
  ldh a, [rJOYP]
  ldh a, [rJOYP] ; this read counts
  or a, $F0 ; A7-4 = 1; A3-0 = unpressed keys
.knownret
  ret



SECTION "Tile data", ROM0

Tiles:
INCBIN "knox.chr"
.End:

SECTION "Tilemap", ROM0

Tilemap:
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0

        ;Random number display
	db $24, $24, $1b, $0a, $17, $0d, $18, $16, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, 0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0

        ;Secret number display
	db $24, $24, $1c, $0e, $0c, $1b, $0e, $1d, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, 0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0

        ;Guess count display
	db $24, $24, $10, $1e, $0e, $1c, $1c, $0e, $1c, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0

        ;Guess number display
	db $24, $24, $10, $1e, $0e, $1c, $1c, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0 
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0

	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
.End:


StrHigher: db $11, $12, $10, $11, $0e, $1b, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24
StrLower: db $15, $18, $20, $0e, $1b, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24
StrCorrect: db $0c, $18, $1b, $1b, $0e, $0c, $1d, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24

SECTION "Variables", WRAM0
wCounterDigitOnes: db
wCounterDigitTens: db

wSecretNumDigitOnes: db
wSecretNumDigitTens: db

wGuessNumDigitOnes: db
wGuessNumDigitTens: db

wGuessCountDigitOnes: db
wGuessCountDigitTens: db

wResult: db

wMode: db

SECTION "Input Variables", WRAM0
wCurKeys: db
wNewKeys: db
