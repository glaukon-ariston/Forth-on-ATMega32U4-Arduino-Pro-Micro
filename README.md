# How to Install Forth on ATMega32U4 SparkFun's Pro Micro
How to install Forth on AVR's ATMega32U4 board (SparkFun's [Pro Micro](https://learn.sparkfun.com/tutorials/pro-micro--fio-v3-hookup-guide/all#hardware-overview-pro-micro) Arduino board)

## On Windows

### How to Restore Pro Micro's Bootloader

Once you put Forth on Pro Micro you will lose the [Arduino bootloader](https://github.com/jknofe/caterina) and will not be able to upload any Arduino sketch. In order to bring it back connect the board to a programmer (I have a USBASP clone from China) and upload the original bootloader. I did it using [avrdudess](https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/) which is a GUI for `avrdude` (`avrdude` comes packaged within).

```shell
pushd C:\app\electro\AVRDUDESS
avrdude -u -c usbasp-clone -p m32u4 -U flash:w:"ArduinoLeonardoBootloader.hex":i -U eeprom:w:"ArduinoLeonardoBootloader.epp":i -U lfuse:w:0xFF:m -U hfuse:w:0xD8:m -U efuse:w:0xCB:m -U lock:w:0x2F:m
```

### Install Precompiled Hex File
- Download [FlashForth](https://flashforth.com/index.html) from [here](http://www.sourceforge.net/projects/flashforth)
- Unzip the ZIP archive `ff5.0.zip` into the project directory, e.g. `c:\avr\`
- Locate `32u4-16MHz-USB.hex` in the project directory (e.g.  `c:\avr\flashforth\avr\hex\32u4-16MHz-USB.hex`)
- Upload the hex file using the USBasp or similar programmer
- `avrdude -u -c usbasp-clone -p m32u4 -U flash:w:"c:\avr\flashforth\avr\hex\32u4-16MHz-USB.hex":a -U lfuse:w:0xFF:m -U hfuse:w:0xDF:m -U efuse:w:0xFF:m`
- Plug the ATMega32U4 board into the PC and you should be able to connect to it using a terminal application (e.g. [TeraTerm](https://ttssh2.osdn.jp/index.html.en) works well, also [PuTTY](https://www.putty.org/) and [RealTerm](https://sourceforge.net/projects/realterm/))
- Press ENTER and you should be greeted with an `ok` prompt. Type `words<ENTER>` and it should print out its dictionary of known words.

```log
  ok<#,ram>
words
p2+ pc@ @p hi d. ud. d> d< d= d0< d0= dinvert d2* d2/ d- d+ dabs ?dnegate dnegate s>d rdrop endit next for in, inline repeat while again until begin then else if zfl pfl xa> >xa x>r dump .s words >pr .id ms ticks r0 s0 latest state bl 2- ['] -@ ; :noname : ] [ does> postpone create cr [char] ihere ( char ' lit abort" ?abort ?abort? abort prompt quit true false .st inlined immediate shb interpret 'source >in tiu tib ti# number? >number ud/mod ud* sign? digit? find immed? (f) c>n n>c @+ c@+ place cmove word parse \ /string source user base pad hp task ulink rsave bin hex decimal . u.r u. sign #> #s # digit <# hold up min max ?negate tuck nip / u*/mod u/ * u/mod um/mod um* 'key? 'key 'emit p++ p+ pc! p! p@ r>p !p>r !p u> u< > < = 0< 0= <> within +! 2/ 2* >body 2+ 1- 1+ negate invert xor or and - m+ + abs dup r@ r> >r rot over swap drop allot ." ," s" (s" type accept 1 umax umin spaces space 2swap 2dup 2drop 2! 2@ cf, chars char+ cells cell+ aligned align cell c, , here dp ram eeprom flash >< rp@ sp! sp@ 2constant constant 2variable variable @ex execute key? key emit Fcy mtst scan skip n= rshift lshift mclr mset ic, i, operator iflush cwd wd- wd+ pause turnkey to is defer value fl+ fl- c! c@ @ a> ! >a literal int! ;i di ei ver warm txu rxu rxu? usb- usb+ empty rx0? rx0 tx0 load- load+ busy idle exit
marker  ok<#,ram>
```

### Compile Your Own Hex File
- Download [FlashForth](https://flashforth.com/index.html) from [here](http://www.sourceforge.net/projects/flashforth).
- Unzip the ZIP archive `ff5.0.zip` into the project directory.
- Follow the instructions as described [here](https://flashforth.com/atmega.html).
- Install [MPLABX](https://www.microchip.com/en-us/development-tools-tools-and-software/mplab-x-ide) and [XC8](https://www.microchip.com/en-us/development-tools-tools-and-software/mplab-xc-compilers).
- Create a new project for ATMega32U4 in MPLABX and add `avr/src/ff-xc8.asm` as the only source file.
![MPLABX IDE](MPLAB_X_IDE.PNG)
- Open project configuration dialog box.
![Project Configuration](MPLAB_X_IDE_Configuration.PNG)
- Add compiler option for the include directory.
![Project Configuration - Compiler](MPLAB_X_IDE_Configuration_Compiler.PNG)
- Add linker option `-nostartfiles`.
![Project Configuration - Compiler](MPLAB_X_IDE_Configuration_Linker.PNG)
- Configure options according to your hardware and preferences in `avr/src/config-xc8.inc`. In my case I set `#define OPERATOR_UART 3` on line 18 indicating I want USB as the UART used for the operator.
- Build the project `Production | Build Main Project`.
- You should get the output similar to the following and the output `hex` file should be in the `dist/default/production/` subdirectory.
```log
make -f nbproject/Makefile-default.mk SUBPROJECTS= .build-conf
make[1]: Entering directory 'D:/RE/Electronics/Arduino/Forth/flashforth/avr/src'
make  -f nbproject/Makefile-default.mk dist/default/production/src.production.hex
make[2]: Entering directory 'D:/RE/Electronics/Arduino/Forth/flashforth/avr/src'
"C:\app\electro\Microchip\xc8\v2.32\bin\xc8-cc.exe" -c  -mcpu=ATmega32U4  -x assembler-with-cpp -D__ATmega32U4__   -mdfp="C:/app/electro/Microchip/MPLABX/v5.50/packs/Microchip/ATmega_DFP/2.3.126/xc8"  -Wl,--gc-sections -O1 -ffunction-sections -fdata-sections -fshort-enums -funsigned-char -funsigned-bitfields -Wall -DXPRJ_default=default  -gdwarf-3 -Wa,--defsym=__MPLAB_BUILD=1 -I.   -MD -MP -MF "build/default/production/ff-xc8.o.d" -MT "build/default/production/ff-xc8.o.d" -MT build/default/production/ff-xc8.o -o build/default/production/ff-xc8.o  ff-xc8.asm 
"C:\app\electro\Microchip\xc8\v2.32\bin\xc8-cc.exe"  -mcpu=ATmega32U4 -Wl,-Map=dist/default/production/src.production.map  -DXPRJ_default=default  -Wl,--defsym=__MPLAB_BUILD=1   -mdfp="C:/app/electro/Microchip/MPLABX/v5.50/packs/Microchip/ATmega_DFP/2.3.126/xc8"  -Wl,--gc-sections -O1 -ffunction-sections -fdata-sections -fshort-enums -funsigned-char -funsigned-bitfields -Wall -gdwarf-3 -nostartfiles      -Wl,--memorysummary,dist/default/production/memoryfile.xml -o dist/default/production/src.production.elf  -o dist/default/production/src.production.elf  build/default/production/ff-xc8.o      -Wl,--start-group  -Wl,-lm -Wl,--end-group 
Info: Loading file: c:\app\electro\microchip\xc8\v2.32\avr\avr\bin\../lib\ldscripts/avr5.xn
"C:\app\electro\Microchip\xc8\v2.32\bin"\\avr-objcopy -O ihex "dist/default/production/src.production.elf" "dist/default/production/src.production.hex"
make[2]: Leaving directory 'D:/RE/Electronics/Arduino/Forth/flashforth/avr/src'
make[1]: Leaving directory 'D:/RE/Electronics/Arduino/Forth/flashforth/avr/src'

BUILD SUCCESSFUL (total time: 1s)
Loading code from D:/RE/Electronics/Arduino/Forth/flashforth/avr/src/dist/default/production/src.production.hex...
Program loaded with pack,ATmega_DFP,2.3.126,Microchip
Loading completed
```
- Upload the `hex` file to the ATMega32U4 board.
```shell
avrdude -u -c usbasp-clone -p m32u4 -U flash:w:"D:\RE\Electronics\Arduino\Forth\flashforth\avr\src\dist\default\production\src.production.hex":a -U lfuse:w:0xFF:m -U hfuse:w:0xDF:m -U efuse:w:0xFF:m
```

### [NOT WORKING] Change the VID and PID and Compile Your Own Hex File
- Download [FlashForth](https://flashforth.com/index.html) [here](http://www.sourceforge.net/projects/flashforth)
- Unzip the ZIP archive `ff5.0.zip` into the project directory, e.g. `c:\avr\`
- Download and install Microchip Studio (ex Atmel Studio)
- Locate `avrasm2.exe` in the Microchip Studio installation (e.g. `C:\app\electro\Atmel\Studio\7.0\toolchain\avr8\avrassembler\`) and copy it into the project directory (e.g.  `c:\avr\flashforth\`)
- Locate `m32U4def.inc` (e.g. `C:\app\electro\Atmel\Studio\7.0\packs\atmel\ATmega_DFP\1.6.364\avrasm\inc`) and copy it to the newly created `include` directory under the project directory (e.g. `c:\avr\flashforth\include`)
- Read the `atmega.sh` from the project directory
- Comment out the definitions of U_VID and U_PID in the source, see `avr\src\usbcdc.asm`
- Run `avrasm2.exe -Dffm32u4 -Dop3 -DU_VID=0x1B4F -DU_PID=0x9205 -I include/ avr/src/ff-atmega.asm -o avr/hex/32u4-16MHz-USB_VID1B4F_PID9205.hex -fI`
- Upload the produced `32u4-16MHz-USB_VID1B4F_PID9205.hex` file using the USBasp or similar programmer
- `avrdude -u -c usbasp-clone -p m32u4 -U flash:w:"c:\avr\flashforth\avr\hex\32u4-16MHz-USB_VID1B4F_PID9205.hex":a -U lfuse:w:0xFF:m -U hfuse:w:0xDF:m -U efuse:w:0xFF:m`
