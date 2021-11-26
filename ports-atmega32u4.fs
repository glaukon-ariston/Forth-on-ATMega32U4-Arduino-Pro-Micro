\ Definition of an input or output (pin)
\ Use:
\ PORTD %10000000 defPIN: PD7  ( define portD pin #7)
: defPIN: ( PORTx mask --- <word> | <word> --- mask port)
    create
        c, c,           \ compile PORT and min mask
    does>
        dup c@          \ push pin mask
        swap 1+ c@      \ push PORT
  ;

\ Action on ports
\ Turn a port pin on, dont change the others.
: high ( pinmask portadr -- )
    mset
  ;
\ Turn a port pin off, dont change the others.
: low ( pinmask portadr -- )
    mclr
  ;

\ Action on DDR registers
\ DDR registers control the direction of data processing. Initially, data ports cannot be used for output.
\ Only for PORTx bits, 
\ because address of DDRx is one less than address of PORTx.
 
\ Set DDRx so its corresponding pin is output.
: output ( pinmask portadr -- )
    1- high
  ;
\ Set DDRx so its corresponding pin is input.
: input  ( pinmask portadr -- )   
    1- low
  ;

\ Pin state recovery
\ read the pins masked as input
: pin@  ( pinmask portaddr -- fl )
    2- mtst \ select PINx register as input
    if      true
    else    false   then
  ;

\ PORTB
37 constant PORTB	\ Port B Data Register
36 constant DDRB	\ Port B Data Direction Register
35 constant PINB	\ Port B Input Pins
\ PORTC
40 constant PORTC	\ Port C Data Register
39 constant DDRC	\ Port C Data Direction Register
38 constant PINC	\ Port C Input Pins
\ PORTD
43 constant PORTD	\ Port D Data Register
42 constant DDRD	\ Port D Data Direction Register
41 constant PIND	\ Port D Input Pins

\ Timer Counter 1
111 constant TIMSK1	\ Timer/Counter Interrupt Mask Register
  $20 constant TIMSK1_ICIE1 \ Timer/Counter1 Input Capture Interrupt Enable
  $04 constant TIMSK1_OCIE1B \ Timer/Counter1 Output CompareB Match Interrupt Enable
  $02 constant TIMSK1_OCIE1A \ Timer/Counter1 Output CompareA Match Interrupt Enable
  $01 constant TIMSK1_TOIE1 \ Timer/Counter1 Overflow Interrupt Enable
54 constant TIFR1	\ Timer/Counter Interrupt Flag register
  $20 constant TIFR1_ICF1 \ Input Capture Flag 1
  $04 constant TIFR1_OCF1B \ Output Compare Flag 1B
  $02 constant TIFR1_OCF1A \ Output Compare Flag 1A
  $01 constant TIFR1_TOV1 \ Timer/Counter1 Overflow Flag
132 constant TCNT1	\ Timer/Counter1  Bytes
136 constant OCR1A	\ Timer/Counter1 Output Compare Register  Bytes
138 constant OCR1B	\ Timer/Counter1 Output Compare Register  Bytes
134 constant ICR1	\ Timer/Counter1 Input Capture Register  Bytes
67 constant GTCCR	\ General Timer/Counter Control Register
  $80 constant GTCCR_TSM \ Timer/Counter Synchronization Mode
  $01 constant GTCCR_PSRSYNC \ Prescaler Reset Timer/Counter1 and Timer/Counter0

\ AD Converter
124 constant ADMUX	\ The ADC multiplexer Selection Register
  $c0 constant ADMUX_REFS \ Reference Selection Bits
  $20 constant ADMUX_ADLAR \ Left Adjust Result
  $0f constant ADMUX_MUX \ Analog Channel and Gain Selection Bits
120 constant ADC	\ ADC Data Register  Bytes
122 constant ADCSRA	\ The ADC Control and Status register A
  $80 constant ADCSRA_ADEN \ ADC Enable
  $40 constant ADCSRA_ADSC \ ADC Start Conversion
  $20 constant ADCSRA_ADATE \ ADC  Auto Trigger Enable
  $10 constant ADCSRA_ADIF \ ADC Interrupt Flag
  $08 constant ADCSRA_ADIE \ ADC Interrupt Enable
  $07 constant ADCSRA_ADPS \ ADC  Prescaler Select Bits
123 constant ADCSRB	\ The ADC Control and Status register B
  $40 constant ADCSRB_ACME \
  $07 constant ADCSRB_ADTS \ ADC Auto Trigger Source bits
126 constant DIDR0	\ Digital Input Disable Register
  $20 constant DIDR0_ADC5D \
  $10 constant DIDR0_ADC4D \
  $08 constant DIDR0_ADC3D \
  $04 constant DIDR0_ADC2D \
  $02 constant DIDR0_ADC1D \
  $01 constant DIDR0_ADC0D \

\ External Interrupts
105 constant EICRA	\ External Interrupt Control Register
  $0C constant EICRA_ISC1 \ External Interrupt Sense Control 1 Bits
  $03 constant EICRA_ISC0 \ External Interrupt Sense Control 0 Bits
61 constant EIMSK	\ External Interrupt Mask Register
  $03 constant EIMSK_INT \ External Interrupt Request 1 Enable
60 constant EIFR	\ External Interrupt Flag Register
  $03 constant EIFR_INTF \ External Interrupt Flags
104 constant PCICR	\ Pin Change Interrupt Control Register
  $07 constant PCICR_PCIE \ Pin Change Interrupt Enables
109 constant PCMSK2	\ Pin Change Mask Register 2
  $FF constant PCMSK2_PCINT \ Pin Change Enable Masks
108 constant PCMSK1	\ Pin Change Mask Register 1
  $7F constant PCMSK1_PCINT \ Pin Change Enable Masks
107 constant PCMSK0	\ Pin Change Mask Register 0
  $FF constant PCMSK0_PCINT \ Pin Change Enable Masks
59 constant GICR	\ Pin Change Interrupt Flag Register
  $07 constant PCIFR_PCIF \ Pin Change Interrupt Flags

