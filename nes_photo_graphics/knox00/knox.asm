  .inesprg 1   ; 1x 16KB PRG code
  .ineschr 1   ; 1x  8KB CHR data
  .inesmap 0   ; mapper 0 = NROM, no bank swapping
  .inesmir 1   ; background mirroring
  

;;;;;;;;;;;;;;;

    
  .bank 0
  .org $C000 
RESET:
  SEI          ; disable IRQs
  CLD          ; disable decimal mode
  LDX #$40
  STX $4017    ; disable APU frame IRQ
  LDX #$FF
  TXS          ; Set up stack
  INX          ; now X = 0
  STX $2000    ; disable NMI
  STX $2001    ; disable rendering
  STX $4010    ; disable DMC IRQs

vblankwait1:       ; First wait for vblank to make sure PPU is ready
  BIT $2002
  BPL vblankwait1

clrmem:
  LDA #$00
  STA $0000, x
  STA $0100, x
  STA $0300, x
  STA $0400, x
  STA $0500, x
  STA $0600, x
  STA $0700, x
  LDA #$FE
  STA $0200, x
  INX
  BNE clrmem
   
vblankwait2:      ; Second wait for vblank, PPU is ready after this
  BIT $2002
  BPL vblankwait2


LoadPalettes:
  LDA $2002             ; read PPU status to reset the high/low latch
  LDA #$3F
  STA $2006             ; write the high byte of $3F00 address
  LDA #$00
  STA $2006             ; write the low byte of $3F00 address
  LDX #$00              ; start out at 0
LoadPalettesLoop:
  LDA palette, x        ; load data from address (palette + the value in x)
                          ; 1st time through loop it will load palette+0
                          ; 2nd time through loop it will load palette+1
                          ; 3rd time through loop it will load palette+2
                          ; etc
  STA $2007             ; write to PPU
  INX                   ; X = X + 1
  CPX #$20              ; Compare X to hex $10, decimal 16 - copying 16 bytes = 4 sprites
  BNE LoadPalettesLoop  ; Branch to LoadPalettesLoop if compare was Not Equal to zero
                        ; if compare was equal to 32, keep going down



LoadSprites:
  LDX #$00              ; start at 0
LoadSpritesLoop:
  LDA sprites, x        ; load data from address (sprites +  x)
  STA $0200, x          ; store into RAM address ($0200 + x)
  INX                   ; X = X + 1
  CPX #$10              ; Compare X to hex $10, decimal 16
  BNE LoadSpritesLoop   ; Branch to LoadSpritesLoop if compare was Not Equal to zero
                        ; if compare was equal to 16, keep going down
           
LoadBackground01:
  LDA $2002             ; read PPU status to reset the high/low latch
  LDA #$20
  STA $2006             ; write the high byte of $2000 address
  LDA #$00
  STA $2006             ; write the low byte of $2000 address
  LDX #$00              ; start out at 0
LoadBackgroundLoop01:
  LDA background01, x     ; load data from address (background + the value in x)
  STA $2007             ; write to PPU
  INX                   ; X = X + 1
  CPX #$00              ; Compare X to hex $80, decimal 128 - copying 128 bytes
  BNE LoadBackgroundLoop01  ; Branch to LoadBackgroundLoop if compare was Not Equal to zero
                        ; if compare was equal to 128, keep going down

LoadBackground02:
;  LDA $2002             ; read PPU status to reset the high/low latch
;  LDA #$20
;  STA $2006             ; write the high byte of $2000 address
;  LDA #$00
;  STA $2006             ; write the low byte of $2000 address
  LDX #$00              ; start out at 0
LoadBackgroundLoop02:
  LDA background02, x     ; load data from address (background + the value in x)
  STA $2007             ; write to PPU
  INX                   ; X = X + 1
  CPX #$00              ; Compare X to hex $80, decimal 128 - copying 128 bytes
  BNE LoadBackgroundLoop02  ; Branch to LoadBackgroundLoop if compare was Not Equal to zero
                        ; if compare was equal to 128, keep going down
              
LoadAttribute:
  LDA $2002             ; read PPU status to reset the high/low latch
  LDA #$23
  STA $2006             ; write the high byte of $23C0 address
  LDA #$C0
  STA $2006             ; write the low byte of $23C0 address
  LDX #$00              ; start out at 0
LoadAttributeLoop:
  LDA attribute, x      ; load data from address (attribute + the value in x)
  STA $2007             ; write to PPU
  INX                   ; X = X + 1
 ; CPX #$08              
  CPX #$20              
  BNE LoadAttributeLoop  ; Branch to LoadAttributeLoop if compare was Not Equal to zero
                        ; if compare was equal to 128, keep going down
              
  LDA #%10010000   ; enable NMI, sprites from Pattern Table 0, background from Pattern Table 1
  STA $2000

  LDA #%00011110   ; enable sprites, enable background, no clipping on left side
  STA $2001

Forever:
  JMP Forever     ;jump back to Forever, infinite loop

NMI:
  LDA #$00
  STA $2003       ; set the low byte (00) of the RAM address
  LDA #$02
  STA $4014       ; set the high byte (02) of the RAM address, start the transfer


LatchController:
  LDA #$01
  STA $4016
  LDA #$00
  STA $4016       ; tell both the controllers to latch buttons


ReadA: 
  LDA $4016       ; player 1 - A
  AND #%00000001  ; only look at bit 0
  BEQ ReadADone   ; branch to ReadADone if button is NOT pressed (0)
                  ; add instructions here to do something when button IS pressed (1)
  LDA $0203       ; load sprite X position
  CLC             ; make sure the carry flag is clear
  ADC #$01        ; A = A + 1
  STA $0203       ; save sprite X position
ReadADone:        ; handling this button is done
  

ReadB: 
  LDA $4016       ; player 1 - B
  AND #%00000001  ; only look at bit 0
  BEQ ReadBDone   ; branch to ReadBDone if button is NOT pressed (0)
                  ; add instructions here to do something when button IS pressed (1)
  LDA $0203       ; load sprite X position
  SEC             ; make sure carry flag is set
  SBC #$01        ; A = A - 1
  STA $0203       ; save sprite X position
ReadBDone:        ; handling this button is done


  ;;This is the PPU clean up section, so rendering the next frame starts properly.
  LDA #%10010000   ; enable NMI, sprites from Pattern Table 0, background from Pattern Table 1
  STA $2000
  LDA #%00011110   ; enable sprites, enable background, no clipping on left side
  STA $2001
  LDA #$00        ;;tell the ppu there is no background scrolling
  STA $2005
  STA $2005
  
  RTI             ; return from interrupt
 
;;;;;;;;;;;;;;  
  
  
  
  .bank 1
  .org $E000
palette:
  .db $0f,$37,$27,$07,  $39,$29,$19,$09,  $22,$3C,$2C,$1C,  $22,$38,$20,$10   ;;background palette
  .db $2d,$1C,$15,$14,  $0f,$02,$38,$3C,  $0f,$1C,$15,$14,  $0f,$02,$38,$3C   ;;sprite palette


sprites:
     ;vert tile attr horiz
  .db $80, $32, $00, $80   ;sprite 0
  .db $80, $33, $00, $88   ;sprite 1
  .db $88, $34, $00, $80   ;sprite 2
  .db $88, $35, $00, $88   ;sprite 3


background01:
  .db $00,$00,$00,$00,$00,$00,$00,$00,$00,$01,$02,$03,$04,$05,$06,$07
  .db $08,$09,$0A,$0B,$0C,$0D,$0E,$0F,$00,$00,$00,$00,$00,$00,$00,$00

  .db $00,$00,$00,$00,$00,$00,$00,$00,$10,$11,$12,$13,$14,$15,$16,$17
  .db $18,$19,$1A,$1B,$1C,$1D,$1E,$1F,$00,$00,$00,$00,$00,$00,$00,$00

  .db $00,$00,$00,$00,$00,$00,$00,$00,$20,$21,$22,$23,$24,$25,$26,$27
  .db $28,$29,$2A,$2B,$2C,$2D,$2E,$2F,$00,$00,$00,$00,$00,$00,$00,$00

  .db $00,$00,$00,$00,$00,$00,$00,$00,$30,$31,$32,$33,$34,$35,$36,$37
  .db $38,$39,$3A,$3B,$3C,$3D,$3E,$3F,$00,$00,$00,$00,$00,$00,$00,$00

  .db $00,$00,$00,$00,$00,$00,$00,$00,$40,$41,$42,$43,$44,$45,$46,$47
  .db $48,$49,$4A,$4B,$4C,$4D,$4E,$4F,$00,$00,$00,$00,$00,$00,$00,$00

  .db $00,$00,$00,$00,$00,$00,$00,$00,$50,$51,$52,$53,$54,$55,$56,$57
  .db $58,$59,$5A,$5B,$5C,$5D,$5E,$5F,$00,$00,$00,$00,$00,$00,$00,$00

  .db $00,$00,$00,$00,$00,$00,$00,$00,$60,$61,$62,$63,$64,$65,$66,$67
  .db $68,$69,$6A,$6B,$6C,$6D,$6E,$6F,$00,$00,$00,$00,$00,$00,$00,$00

  .db $00,$00,$00,$00,$00,$00,$00,$00,$70,$71,$72,$73,$74,$75,$76,$77
  .db $78,$79,$7A,$7B,$7C,$7D,$7E,$7F,$00,$00,$00,$00,$00,$00,$00,$00

background02:
  .db $00,$00,$00,$00,$00,$00,$00,$00,$80,$81,$82,$83,$84,$85,$86,$87
  .db $88,$89,$8A,$8B,$8C,$8D,$8E,$8F,$00,$00,$00,$00,$00,$00,$00,$00

  .db $00,$00,$00,$00,$00,$00,$00,$00,$90,$91,$92,$93,$94,$95,$96,$97
  .db $98,$99,$9A,$9B,$9C,$9D,$9E,$9F,$00,$00,$00,$00,$00,$00,$00,$00

  .db $00,$00,$00,$00,$00,$00,$00,$00,$A0,$A1,$A2,$A3,$A4,$A5,$A6,$A7
  .db $A8,$A9,$AA,$AB,$AC,$AD,$AE,$AF,$00,$00,$00,$00,$00,$00,$00,$00

  .db $00,$00,$00,$00,$00,$00,$00,$00,$B0,$B1,$B2,$B3,$B4,$B5,$B6,$B7
  .db $B8,$B9,$BA,$BB,$BC,$BD,$BE,$BF,$00,$00,$00,$00,$00,$00,$00,$00

  .db $00,$00,$00,$00,$00,$00,$00,$00,$C0,$C1,$C2,$C3,$C4,$C5,$C6,$C7
  .db $C8,$C9,$CA,$CB,$CC,$CD,$CE,$CF,$00,$00,$00,$00,$00,$00,$00,$00

  .db $00,$00,$00,$00,$00,$00,$00,$00,$D0,$D1,$D2,$D3,$D4,$D5,$D6,$D7
  .db $D8,$D9,$DA,$DB,$DC,$DD,$DE,$DF,$00,$00,$00,$00,$00,$00,$00,$00

  .db $00,$00,$00,$00,$00,$00,$00,$00,$E0,$E1,$E2,$E3,$E4,$E5,$E6,$E7
  .db $E8,$E9,$EA,$EB,$EC,$ED,$EE,$EF,$00,$00,$00,$00,$00,$00,$00,$00

  .db $00,$00,$00,$00,$00,$00,$00,$00,$F0,$F1,$F2,$F3,$F4,$F5,$F6,$F7
  .db $F8,$F9,$FA,$FB,$FC,$FD,$FE,$FF,$00,$00,$00,$00,$00,$00,$00,$00


attribute:
  .db %11111111, %11111111, %00000000, %00000000, %10001010, %10101010, %11111111, %11111111
  .db %11111111, %11111111, %00000000, %00000000, %10001000, %10101010, %11111111, %11111111

  .db %11111111, %11111111, %00000000, %00000000, %00000100, %01010101, %11111111, %11111111
  .db %11111111, %11111111, %00000000, %00000000, %00000000, %00000100, %11111111, %11111111



  .org $FFFA     ;first of the three vectors starts here
  .dw NMI        ;when an NMI happens (once per frame if enabled) the 
                   ;processor will jump to the label NMI:
  .dw RESET      ;when the processor first turns on or is reset, it will jump
                   ;to the label RESET:
  .dw 0          ;external interrupt IRQ is not used in this tutorial
  
  
;;;;;;;;;;;;;;  
  
  
  .bank 2
  .org $0000
  .incbin "knox.chr"   ;includes 8KB graphics file from SMB1