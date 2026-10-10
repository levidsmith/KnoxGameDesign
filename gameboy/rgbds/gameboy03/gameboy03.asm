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

	;Setup code for VBlank
;Init:
;	ld a, $40     ; VBlank Interrupt
;	ldh [rIE], a
;	ld a, 0
;	ldh [rIF], a
;	ei

	; Do not turn the LCD off outside of VBlank
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
	ld [myCounterDigitOnes], a
	ld [myCounterDigitTens], a

	call IncrementCounter	

Done:
	ld a, [rLY]
	cp 144
	jr c, NotInVBlank

	call IncrementCounter

NotInVBlank:

	jr Done


;SECTION "VBlank Interrupt", ROM0[$0040]
;VBlankInterrupt:
;	reti


IncrementCounter:		
	ld a, [myCounterDigitOnes]
	inc a
	ld [myCounterDigitOnes], a

	ld a, [myCounterDigitOnes]
	cp a, $a                  ; compare ones digit with 10 ($a)
	jp nz, CounterResetTensDone       ; reset ones digit to 0 if equal to 10.  otherwise do jump to end of reset code

CounterResetOnes:
    ld a, 0
	ld [myCounterDigitOnes], a

    ld a, [myCounterDigitTens]
	inc a
	ld [myCounterDigitTens], a

	cp a, $a   ; compare tens digit with 10 ($a)
	jp nz, CounterResetTensDone

CounterResetOnesDone:

CounterResetTens:
	ld a, 0
	ld [myCounterDigitTens], a
CounterResetTensDone:

	
	ld hl, $9840
	ld a, [myCounterDigitTens]
	ld [hli], a
	ld a, [myCounterDigitOnes]
	ld [hli], a
    ret



SECTION "Tile data", ROM0

Tiles:
INCBIN "knox.chr"
.End:

SECTION "Tilemap", ROM0

Tilemap:
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $0c, $18, $1e, $17, $1d, $0e, $1b, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0
	db $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24,  0,0,0,0,0,0,0,0,0,0,0,0

.End:


;SECTION "Frame Counter", HRAM
;hFrameCounter: db

;SECTION "VBlank Handler", ROM0
;VBlankHandler:
;	ldh a, [hFrameCounter]
;	call IncrementCounter

SECTION "Counter", WRAM0
myCounterDigitOnes: db
myCounterDigitTens: db