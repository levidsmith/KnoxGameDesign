  .inesprg 1   
  .ineschr 1   
  .inesmap 0   
  .inesmir 1   

;game variables  
  .rsset $0000

gamestate        .rs 1
numsecret1       .rs 1
numsecret2       .rs 1
numguess1        .rs 1
numguess2        .rs 1
numguesscount    .rs 1
buttons1         .rs 1
buttons1previous .rs 1

STATETITLE = $00
STATEGUESS = $01

  .bank 0
  .org $C000 
RESET:
  SEI          
  CLD          
  LDX #$40
  STX $4017    
  LDX #$FF
  TXS          
  INX          
  STX $2000    
  STX $2001    
  STX $4010    

vblankwait1:       
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
   
vblankwait2:      
  BIT $2002
  BPL vblankwait2

LoadPalettes:
  LDA $2002             
  LDA #$3F
  STA $2006             
  LDA #$00
  STA $2006             
  LDX #$00              
LoadPalettesLoop:
  LDA palette, x        



  STA $2007             
  INX                   
  CPX #$20              
  BNE LoadPalettesLoop  
                        
LoadSprites:
  LDX #$00              
LoadSpritesLoop:
  LDA sprites, x        
  STA $0200, x          
  INX                   
  CPX #$38              
  BNE LoadSpritesLoop   
           
LoadBackground01:
  LDA $2002             
  LDA #$20
 STA $2006             
  LDA #$00
  STA $2006             
  LDX #$00              
LoadBackgroundLoop01:
  LDA #$24
  STA $2007             
  INX                   
  CPX #$00              
  BNE LoadBackgroundLoop01  

LoadBackground02:
  LDX #$00             
LoadBackgroundLoop02:
  LDA #$24
  STA $2007            
  INX                  
  CPX #$00             
  BNE LoadBackgroundLoop02 

LoadBackground03:
  LDX #$00             
LoadBackgroundLoop03:
  LDA #$24
  STA $2007            
  INX                  
  CPX #$00             
  BNE LoadBackgroundLoop03

LoadBackground04:
  LDX #$00
LoadBackgroundLoop04:
  LDA #$24
  STA $2007            
  INX                  
  CPX #$c0             
  BNE LoadBackgroundLoop04
              
LoadAttribute:
  LDA $2002             
  LDA #$23
  STA $2006             
  LDA #$C0
  STA $2006             
  LDX #$00              
LoadAttributeLoop:
  LDA attribute, x      
  STA $2007             
  INX                   
 ; CPX #$08              
  CPX #$40              
  BNE LoadAttributeLoop  
              
  LDA #%10010000   
  STA $2000

  LDA #%00011110  
  STA $2001

Forever:
  JMP Forever     

NMI:
  LDA #$00
  STA $2003       
  LDA #$02
  STA $4014 

  JSR DrawScore
  LDA #%10010000   
;  LDA #%10000000   
  STA $2000
  LDA #%00011110   
  STA $2001
  LDA #$00        
  STA $2005
  STA $2005
  JSR ReadController1

GameEngine:
  LDA gamestate
  CMP #$00
  BEQ EngineTitle

  LDA gamestate
  CMP #$01
  BEQ EngineInput1

  LDA gamestate
  CMP #$02
  BEQ EngineInput2

  LDA gamestate
  CMP #$03
  BEQ EngineInput3
GameEngineDone:

  RTI  

EngineTitle:
  JSR RandomizeSecret

  LDA buttons1
  CMP #$10           ; START button = 16
  BNE GameEngineDone
;  JMP GameEngineDone
  LDA #$01
  STA gamestate
  JMP GameEngineDone

EngineInput1:
  LDA buttons1previous
  CMP #$00
  BNE GameEngineDone

EngineInput1A:
  LDA buttons1
  CMP #$80           ; A button = 128
  BNE EngineInput1ADone

  LDA #$02
  STA gamestate
EngineInput1ADone:

EngineInput1Up:
  LDA buttons1
  CMP #$08           ; Up button = 8
  BNE EngineInput1UpDone

  LDA numguess1
  CLC
  ADC #$01
  STA numguess1
  CMP #$0a
  BNE EngineInput1UpDone
  LDA #$00
  STA numguess1
EngineInput1UpDone:

  JMP GameEngineDone

EngineInput2:
  LDA buttons1previous
  CMP #$00
  BNE GameEngineDone

EngineInput2A:
  LDA buttons1
  CMP #$80           ; A button = 128
  BNE EngineInput2ADone

;  LDA #$02
;  STA numguess2

  LDA #$03
  STA gamestate
EngineInput2ADone:

EngineInput2Up:
  LDA buttons1
  CMP #$08           ; Up button = 8
  BNE EngineInput2UpDone

  LDA numguess2
  CLC
  ADC #$01
  STA numguess2
  CMP #$0a
  BNE EngineInput2UpDone
  LDA #$00
  STA numguess2
EngineInput2UpDone:

  JMP GameEngineDone




EngineInput3:
  LDA buttons1previous
  CMP #$00
  BNE GameEngineDone

  LDA buttons1
  CMP #$80           ; A button = 128
  BNE GameEngineDone

  LDA #$00
  STA numguess1
  STA numguess2

  LDA #$01
  STA gamestate
  JMP GameEngineDone



;continually increment two values 0 to 9
;until the user presses start
RandomizeSecret:
  LDA numsecret1
  CLC
  ADC #$01
  STA numsecret1

  CMP #$0a
  BNE RandomizeDone

  LDA #$00
  STA numsecret1
  LDA numsecret2
  CLC
  ADC #$01
  STA numsecret2

  CMP #$0a
  BNE RandomizeDone
  
  LDA #$00
  STA numsecret2

RandomizeDone:
  RTS

ReadController1:
  LDA buttons1
  STA buttons1previous

  LDA #$01
  STA $4016
  LDA #$00
  STA $4016
  LDX #$08
ReadController1Loop:
  LDA $4016
  LSR A            ; bit0 -> Carry
  ROL buttons1     ; bit0 <- Carry
  DEX
  BNE ReadController1Loop
  RTS

 
  .bank 1
  .org $E000
palette:
  .db $00,$30,$0f,$0f,  $0f,$16,$0f,$0f,  $00,$19,$00,$00,  $00,$00,$00,$00
  .db $12,$0f,$0f,$0f,  $0f,$0f,$0f,$0f,  $0f,$0f,$0f,$0f,  $0f,$0f,$0f,$0f


sprites:
  .db $A0, $14, $00, $80 
  .db $A0, $17, $00, $88
  .db $A0, $18, $00, $90
  .db $A0, $21, $00, $98

  .db $B0, $10, $01, $80 
  .db $B0, $0a, $01, $88
  .db $B0, $16, $01, $90
  .db $B0, $0e, $01, $98

  .db $C0, $0d, $02, $80 
  .db $C0, $0e, $02, $88
  .db $C0, $1c, $02, $90
  .db $C0, $12, $02, $98
  .db $C0, $10, $02, $a0
  .db $C0, $17, $02, $a8

strpressstart:
  .db $19,$1b,$0e,$1c,$1c,$24,$1c,$1d,$0a,$1b,$1d

strgensecnum:
  .db $10,$0e,$17,$0e,$1b,$0a,$1d,$12,$17,$10,$24,$1c,$0e,$0c,$1b,$0e,$1d,$24,$17,$1e,$16,$0b,$0e,$1b

strguessnum:
  .db $10,$1e,$0e,$1c,$1c,$24,$17,$1e,$16,$0b,$0e,$1b

attribute:
   .db %00000000, %00000000, %00000000, %00000000, %00000000, %00000000, %00000000, %00000000
   .db %00000000, %00000000, %00000000, %00000000, %00000000, %00000000, %00000000, %00000000

   .db %00000000, %00000000, %00000000, %00000000, %00000000, %00000000, %00000000, %00000000
   .db %00000000, %00000000, %00000000, %00000000, %00000000, %00000000, %00000000, %00000000

   .db %00000000, %00000000, %00000000, %00000000, %00000000, %00000000, %00000000, %00000000
   .db %00000000, %00000000, %00000000, %00000000, %00000000, %00000000, %00000000, %00000000

   .db %00000000, %00000000, %00000000, %00000000, %00000000, %00000000, %00000000, %00000000
   .db %00000000, %00000000, %00000000, %00000000, %00000000, %00000000, %00000000, %00000000


DrawScore:


;text: GENERATING SECRET NUMBER
  LDA $2002             
  LDA #$20
  STA $2006             
  LDA #$44  ;row 3 column 4
  STA $2006             
  LDX #$00              
TextLoop01:
  LDA strgensecnum, x
  STA $2007             
  INX                   
  CPX #$18
  BNE TextLoop01  

;text: PRESS START
  LDA $2002             
  LDA #$20
  STA $2006             
  LDA #$64  ;row 4 column 4
  STA $2006             
  LDX #$00              
TextLoop02:
  LDA strpressstart, x
  STA $2007             
  INX                   
  CPX #$0b
  BNE TextLoop02

;text: secret number
;first digit
    LDA $2002
    LDA #$20
    STA $2006
    LDA #$A4  ;row 6 column 4
    STA $2006

    LDA numsecret1
    STA $2007

;second digit
  LDA numsecret2
  STA $2007  

;text: guess number
  LDA $2002             
  LDA #$20
  STA $2006             
  LDA #$e4  ;row 8 column 4
  STA $2006             
  LDX #$00              
TextLoop03:
  LDA strguessnum, x
  STA $2007             
  INX                   
  CPX #$0c
  BNE TextLoop03

;text: numguess1 (first digit)
    LDA $2002
    LDA #$21
    STA $2006
    LDA #$04
    STA $2006

    LDA numguess1
    STA $2007

;text; numguess2 (second digit)
  LDA numguess2
  STA $2007  


    RTS


  .org $FFFA
  .dw NMI   
            
  .dw RESET 
            
  .dw 0     
  
  
  .bank 2
  .org $0000
  .incbin "numguess.chr"  