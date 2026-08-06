               processor 6502
               include "vcs.h"
               include "macro.h"
;------------------------------------------------------------------------------

PATTERN_INDEX      = $80               
PATTERN1_K         = $81                  
PATTERN2_K         = $82                  
PATTERN1_N         = $83                  
PATTERN2_N         = $84                  
PATTERN1_O         = $85                  
PATTERN2_O         = $86                  
PATTERN1_X         = $87                  
PATTERN2_X         = $88                  

TIMETOCHANGE    = $78                   ; $78 = 2 seconds
;------------------------------------------------------------------------------

               SEG
               ORG $F000

Reset

  ; Clear RAM and all TIA registers

               ldx #0 
               lda #0 
Clear           sta 0,x 
               inx 

               bne Clear

      ;------------------------------------------------

	  
	  ; K PATTERN: -.-
               lda #235
               sta PATTERN1_K
               lda #1
               sta PATTERN2_K

	  ; N PATTERN: -.
               lda #232
               sta PATTERN1_N
               lda #0
               sta PATTERN2_N

	  ; O PATTERN: ---
               lda #238
               sta PATTERN1_O
               lda #7
               sta PATTERN2_O

	  ; X PATTERN: -..-
               lda #234
               sta PATTERN1_X
               lda #7
               sta PATTERN2_X


	  lda #0
	  sta PATTERN_INDEX 




               lda #$42 ; color red
;               lda #$b2 ; color green
;               lda #$80 ; color blue

               sta COLUPF             ; set the playfield colour
               ldy #0                 ; "speed" counter

      ;------------------------------------------------

StartOfFrame
  ; Start of new frame
  ; Start of vertical blank processing
               lda #0
               sta VBLANK

               lda #2
               sta VSYNC

               sta WSYNC
               sta WSYNC
               sta WSYNC               ; 3 scanlines of VSYNC signal

               lda #0
               sta VSYNC           

      ;------------------------------------------------

      ; 37 scanlines of vertical blank...

               ldx #0

VerticalBlank   sta WSYNC

               inx

               cpx #37

               bne VerticalBlank

      ;------------------------------------------------

      ; Handle a change in the pattern once every 20 frames

      ; and write the pattern to the PF1 register
               iny                    ; increment speed count by one
		   
               cpy #TIMETOCHANGE      ; has it reached our "change point"?
               bne notyet             ; no, so branch past;

               ldy #0                 ; reset speed count;
			   
			   inc PATTERN_INDEX
			   lda PATTERN_INDEX
			   cmp #4
			   bne notyet
			   lda #0
			   sta PATTERN_INDEX

notyet
               lda PATTERN_INDEX
			   cmp #0
			   beq draw_k
			   
			   cmp #1
			   beq draw_n

			   cmp #2
			   beq draw_o
			   
			   cmp #3
			   beq draw_x
  
			   jmp draw_done

draw_k
               lda PATTERN1_K
               sta PF1

               lda PATTERN2_K
               sta PF2

               ;red
               lda #$42
			   sta COLUPF 
			   
			   jmp draw_done
			   
draw_n			   
               lda PATTERN1_N
               sta PF1

               lda PATTERN2_N
               sta PF2

               ;green
               lda #$c0
			   sta COLUPF 


			   jmp draw_done

draw_o			   
               lda PATTERN1_O
               sta PF1

               lda PATTERN2_O
               sta PF2

               ;blue
               lda #$80
			   sta COLUPF 


			   jmp draw_done

draw_x
               lda PATTERN1_X
               sta PF1

               lda PATTERN2_X
               sta PF2

               ;purple
               lda #$50
			   sta COLUPF 


			   jmp draw_done

draw_done

      ;------------------------------------------------

      ; Do 192 scanlines of colour-changing (our picture)
               ldx #0                 ; this counts our scanline number

Picture1         
			   ;gray background
			   lda #$08 
			   sta COLUBK
			   
               sta WSYNC              ; wait till end of scanline
               inx
               cpx #192
               bne Picture1

      ;------------------------------------------------

               lda #%01000010
               sta VBLANK          ; end of screen - enter blanking

  ; 30 scanlines of overscan...

               ldx #0

Overscan        sta WSYNC

               inx

               cpx #30

               bne Overscan

               jmp StartOfFrame

;------------------------------------------------------------------------------

           ORG $FFFA

InterruptVectors

           .word Reset          ; NMI
           .word Reset          ; RESET
           .word Reset          ; IRQ

     END