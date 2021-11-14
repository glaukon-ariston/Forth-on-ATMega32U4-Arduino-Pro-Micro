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
- Download [FlashForth](https://flashforth.com/index.html) [here](http://www.sourceforge.net/projects/flashforth)
- Unzip the ZIP archive `ff5.0.zip` into the project directory, e.g. `c:\avr\`
- Locate `32u4-16MHz-USB.hex` in the project directory (e.g.  `c:\avr\flashforth\avr\hex\32u4-16MHz-USB.hex`)
- Upload the hex file using the USBasp or similar programmer
- `avrdude -u -c usbasp-clone -p m32u4 -U flash:w:"c:\avr\flashforth\avr\hex\32u4-16MHz-USB.hex":a -U lfuse:w:0xFF:m -U hfuse:w:0xDF:m -U efuse:w:0xFF:m`

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
- `avrdude -u -c usbasp-clone -p m32u4 -e -U flash:w:"c:\avr\flashforth\avr\hex\32u4-16MHz-USB_VID1B4F_PID9205.hex":a -U lfuse:w:0xFF:m -U hfuse:w:0xD9:m -U efuse:w:0xE9:m`
