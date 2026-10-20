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

Done:
	ld a, [rLY]
	cp 144
	jr c, NotInVBlank


NotInVBlank:
	jr Done


SECTION "Tile data", ROM0

Tiles:
INCBIN "ldsmith.chr"
.End:

SECTION "Tilemap", ROM0

Tilemap:
	db 0, 0, 0, 0, 0, 0, $00, $01, $02, $03, $04, $05, $06, $07, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 
	db 0, 0, 0, 0, 0, 0, $10, $11, $12, $13, $14, $15, $16, $17, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 

        db 0, 0, 0, 0, 0, 0, $20, $21, $22, $23, $24, $25, $26, $27, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        db 0, 0, 0, 0, 0, 0, $30, $31, $32, $33, $34, $35, $36, $37, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
	
        db 0, 0, 0, 0, 0, 0, $40, $41, $42, $43, $44, $45, $46, $47, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        db 0, 0, 0, 0, 0, 0, $50, $51, $52, $53, $54, $55, $56, $57, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0

        db 0, 0, 0, 0, 0, 0, $60, $61, $62, $63, $64, $65, $66, $67, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        db 0, 0, 0, 0, 0, 0, $70, $71, $72, $73, $74, $75, $76, $77, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0

        db 0, 0, 0, 0, 0, 0, $08, $09, $0A, $0B, $0C, $0D, $0E, $0F, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        db 0, 0, 0, 0, 0, 0, $18, $19, $1A, $1B, $1C, $1D, $1E, $1F, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
		
        db 0, 0, 0, 0, 0, 0, $28, $29, $2A, $2B, $2C, $2D, $2E, $2F, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        db 0, 0, 0, 0, 0, 0, $38, $39, $3A, $3B, $3C, $3D, $3E, $3F, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0

        db 0, 0, 0, 0, 0, 0, $48, $49, $4A, $4B, $4C, $4D, $4E, $4F, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        db 0, 0, 0, 0, 0, 0, $58, $59, $5A, $5B, $5C, $5D, $5E, $5F, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0

        db 0, 0, 0, 0, 0, 0, $68, $69, $6A, $6B, $6C, $6D, $6E, $6F, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        db 0, 0, 0, 0, 0, 0, $78, $79, $7A, $7B, $7C, $7D, $7E, $7F, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0

        db 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        db 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
.End:

StrName: db $11, $12, $10, $11, $0e, $1b, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24, $24
