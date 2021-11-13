# How to Install Forth on ATMega32U4 SparkFun's Pro Micro
How to install Forth on AVR's ATMega32U4 board (SparkFun's [Pro Micro](https://learn.sparkfun.com/tutorials/pro-micro--fio-v3-hookup-guide/all#hardware-overview-pro-micro) Arduino board)

## On Windows

### How to Restore Pro Micro's Bootloader

Once you put Forth on Pro Micro you will lose the [Arduino bootloader](https://github.com/jknofe/caterina) and will not be able to upload any Arduino sketch. In order to bring it back connect the board to a programmer (I have a USBASP clone from China) and upload the original bootloader. I did it using [avrdudess](https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/) which is a GUI for `avrdude` (`avrdude` comes packaged within).

```shell
pushd C:\app\electro\AVRDUDESS
avrdude -u -c usbasp-clone -p m32u4 -U flash:w:"ArduinoLeonardoBootloader.hex":i -U eeprom:w:"ArduinoLeonardoBootloader.epp":i -U lfuse:w:0xFF:m -U hfuse:w:0xD8:m -U efuse:w:0xCB:m -U lock:w:0x2F:m
```

### [NOT WORKING] Install Precompiled Hex File
- Download [FlashForth](https://flashforth.com/index.html) [here](http://www.sourceforge.net/projects/flashforth)
- Unzip the ZIP archive `ff5.0.zip` into the project directory, e.g. `c:\avr\`
- Locate `32u4-16MHz-USB.hex` in the project directory (e.g.  `c:\avr\flashforth\avr\hex\32u4-16MHz-USB.hex`)
- Upload the hex file using the USBasp or similar programmer
- `avrdude -u -c usbasp-clone -p m32u4 -e -U flash:w:"c:\avr\flashforth\avr\hex\32u4-16MHz-USB.hex":a -U lfuse:w:0xFF:m -U hfuse:w:0xD9:m -U efuse:w:0xE9:m`
- __The result is that the USB device is either not recognised at all or it is recognised with errors__

The following is the [USB Device Tree Viewer](https://www.uwe-sieber.de/usbtreeview_e.html) output when the device is recognised albeit with errors.
```log

    =========================== USB Port2 ===========================

Connection Status        : 0x01 (Device is connected)
Port Chain               : 1-2
Properties               : 0x01
 IsUserConnectable       : yes
 PortIsDebugCapable      : no
 PortHasMultiCompanions  : no
 PortConnectorIsTypeC    : no
ConnectionIndex          : 2
CompanionIndex           : 0
 CompanionHubSymLnk      : USB#ROOT_HUB30#4&2664ba56&0&0#{f18a0e88-c30c-11d0-8815-00a0c906bed8}
 CompanionPortNumber     : 11
 -> CompanionPortChain   : 1-11

      ======================== USB Device ========================

        +++++++++++++++++ Device Information ++++++++++++++++++
Friendly Name            : USB Serial Device (COM13)
Device Description       : USB Serial Device
Device ID                : USB\VID_FAF0&PID_FAF0\5&1779F47&0&2
Hardware IDs             : USB\VID_FAF0&PID_FAF0&REV_0000 USB\VID_FAF0&PID_FAF0
Driver KeyName           : {4d36e978-e325-11ce-bfc1-08002be10318}\0010 (GUID_DEVCLASS_PORTS)
Driver                   : \SystemRoot\System32\drivers\usbser.sys (Version: 10.0.19041.1202  Date: 2021-09-17)
Driver Inf               : C:\WINDOWS\inf\usbser.inf
Legacy BusType           : PNPBus
Class                    : Ports
Class GUID               : {4d36e978-e325-11ce-bfc1-08002be10318} (GUID_DEVCLASS_PORTS)
Service                  : usbser
Enumerator               : USB
Location Info            : Port_#0002.Hub_#0002
Container ID             : {2a24f566-3c15-11ec-b83e-e8b1fc69d704}
Manufacturer Info        : Microsoft
Capabilities             : 0x04 (Removable)
Status                   : 0x01806400 (DN_HAS_PROBLEM, DN_DISABLEABLE, DN_REMOVABLE, DN_NT_ENUMERATOR, DN_NT_DRIVER)
Problem Code             : 10 (CM_PROB_FAILED_START)
HcDisableSelectiveSuspend: 0
EnableSelectiveSuspend   : 0
SelectiveSuspendEnabled  : 0
EnhancedPowerMgmtEnabled : 0
IdleInWorkingState       : 0
WakeFromSleepState       : 0
Power State              : D3 (supported: D0, D3, wake from D0)
COM-Port                 : COM13 (none assigned)

        ---------------- Connection Information ---------------
Connection Index         : 0x02 (2)
Connection Status        : 0x01 (DeviceConnected)
Current Config Value     : 0x00
Device Address           : 0x0C (12)
Is Hub                   : 0x00 (no)
Device Bus Speed         : 0x01 (Full-Speed)
Number Of Open Pipes     : 0x00 (0 pipes to data endpoints)
Data (HexDump)           : 02 00 00 00 12 01 10 01 02 00 00 20 F0 FA F0 FA   ........... ....
                           00 00 00 01 00 01 00 01 00 0C 00 00 00 00 00 01   ................
                           00 00 00                                          ...

        --------------- Connection Information V2 -------------
Connection Index         : 0x02 (2)
Length                   : 0x10 (16 bytes)
SupportedUsbProtocols    : 0x03
 Usb110                  : 1 (yes)
 Usb200                  : 1 (yes)
 Usb300                  : 0 (no)
 ReservedMBZ             : 0x00
Flags                    : 0x00
 DevIsOpAtSsOrHigher     : 0 (Is not operating at SuperSpeed or higher)
 DevIsSsCapOrHigher      : 0 (Is not SuperSpeed capable or higher)
 DevIsOpAtSsPlusOrHigher : 0 (Is not operating at SuperSpeedPlus or higher)
 DevIsSsPlusCapOrHigher  : 0 (Is not SuperSpeedPlus capable or higher)
 ReservedMBZ             : 0x00
Data (HexDump)           : 02 00 00 00 10 00 00 00 03 00 00 00 00 00 00 00   ................

    ---------------------- Device Descriptor ----------------------
bLength                  : 0x12 (18 bytes)
bDescriptorType          : 0x01 (Device Descriptor)
bcdUSB                   : 0x110 (USB Version 1.10)
bDeviceClass             : 0x02 (Communications and CDC Control)
bDeviceSubClass          : 0x00
bDeviceProtocol          : 0x00 (No class specific protocol required)
bMaxPacketSize0          : 0x20 (32 bytes)
idVendor                 : 0xFAF0
idProduct                : 0xFAF0
bcdDevice                : 0x0000
iManufacturer            : 0x00 (No String Descriptor)
iProduct                 : 0x01 (String Descriptor 1)
iSerialNumber            : 0x00 (No String Descriptor)
bNumConfigurations       : 0x01 (1 Configuration)
Data (HexDump)           : 12 01 10 01 02 00 00 20 F0 FA F0 FA 00 00 00 01   ....... ........
                           00 01                                             ..

    ------------------ Configuration Descriptor -------------------
bLength                  : 0x09 (9 bytes)
bDescriptorType          : 0x02 (Configuration Descriptor)
wTotalLength             : 0x003E (62 bytes)
bNumInterfaces           : 0x02 (2 Interfaces)
bConfigurationValue      : 0x01 (Configuration 1)
iConfiguration           : 0x00 (No String Descriptor)
bmAttributes             : 0x80
 D7: Reserved, set 1     : 0x01
 D6: Self Powered        : 0x00 (no)
 D5: Remote Wakeup       : 0x00 (no)
 D4..0: Reserved, set 0  : 0x00
MaxPower                 : 0x32 (100 mA)
Data (HexDump)           : 09 02 3E 00 02 01 00 80 32 09 04 00 00 01 02 02   ..>.....2.......
                           01 00 05 24 00 10 01 04 24 02 02 05 24 06 00 01   ...$....$...$...
                           07 05 82 03 08 00 10 09 04 01 00 02 0A 00 00 00   ................
                           07 05 03 02 08 00 00 07 05 84 02 08 00 00         ..............

        ---------------- Interface Descriptor -----------------
bLength                  : 0x09 (9 bytes)
bDescriptorType          : 0x04 (Interface Descriptor)
bInterfaceNumber         : 0x00
bAlternateSetting        : 0x00
bNumEndpoints            : 0x01 (1 Endpoint)
bInterfaceClass          : 0x02 (Communications and CDC Control)
bInterfaceSubClass       : 0x02 (Abstract Control Model)
bInterfaceProtocol       : 0x01 (AT Commands defined by ITU-T V.250 etc)
iInterface               : 0x00 (No String Descriptor)
Data (HexDump)           : 09 04 00 00 01 02 02 01 00                        .........

        -------------- CDC Interface Descriptor ---------------
bFunctionLength          : 0x05 (5 bytes)
bDescriptorType          : 0x24 (Interface)
bDescriptorSubType       : 0x00 (Header Functional Descriptor)
bcdCDC                   : 0x110 (CDC Version 1.10)
Data (HexDump)           : 05 24 00 10 01                                    .$...

        -------------- CDC Interface Descriptor ---------------
bFunctionLength          : 0x04 (4 bytes)
bDescriptorType          : 0x24 (Interface)
bDescriptorSubType       : 0x02 (Abstract Control Management Functional Descriptor)
bmCapabilities           : 0x02
 D7..4                   : 0x00 (Reserved)
 D3                      : 0x00 (not supports the notification Network_Connection)
 D2                      : 0x00 (not supports the request Send_Break)
 D1                      : 0x01 (supports the request combination of Set_Line_Coding, Set_Control_Line_State, Get_Line_Coding, and the notification Serial_State)
 D0                      : 0x00 (not supports the request combination of Set_Comm_Feature, Clear_Comm_Feature, and Get_Comm_Feature)
Data (HexDump)           : 04 24 02 02                                       .$..

        -------------- CDC Interface Descriptor ---------------
bFunctionLength          : 0x05 (5 bytes)
bDescriptorType          : 0x24 (Interface)
bDescriptorSubType       : 0x06 (Union Functional Descriptor)
bControlInterface        : 0x00
bSubordinateInterface[0] : 0x01
Data (HexDump)           : 05 24 06 00 01                                    .$...

        ----------------- Endpoint Descriptor -----------------
bLength                  : 0x07 (7 bytes)
bDescriptorType          : 0x05 (Endpoint Descriptor)
bEndpointAddress         : 0x82 (Direction=IN EndpointID=2)
bmAttributes             : 0x03 (TransferType=Interrupt)
wMaxPacketSize           : 0x0008 (8 bytes)
bInterval                : 0x10 (16 ms)
Data (HexDump)           : 07 05 82 03 08 00 10                              .......

        ---------------- Interface Descriptor -----------------
bLength                  : 0x09 (9 bytes)
bDescriptorType          : 0x04 (Interface Descriptor)
bInterfaceNumber         : 0x01
bAlternateSetting        : 0x00
bNumEndpoints            : 0x02 (2 Endpoints)
bInterfaceClass          : 0x0A (CDC-Data)
bInterfaceSubClass       : 0x00
bInterfaceProtocol       : 0x00
iInterface               : 0x00 (No String Descriptor)
Data (HexDump)           : 09 04 01 00 02 0A 00 00 00                        .........

        ----------------- Endpoint Descriptor -----------------
bLength                  : 0x07 (7 bytes)
bDescriptorType          : 0x05 (Endpoint Descriptor)
bEndpointAddress         : 0x03 (Direction=OUT EndpointID=3)
bmAttributes             : 0x02 (TransferType=Bulk)
wMaxPacketSize           : 0x0008 (8 bytes)
bInterval                : 0x00 (ignored)
Data (HexDump)           : 07 05 03 02 08 00 00                              .......

        ----------------- Endpoint Descriptor -----------------
bLength                  : 0x07 (7 bytes)
bDescriptorType          : 0x05 (Endpoint Descriptor)
bEndpointAddress         : 0x84 (Direction=IN EndpointID=4)
bmAttributes             : 0x02 (TransferType=Bulk)
wMaxPacketSize           : 0x0008 (8 bytes)
bInterval                : 0x00 (ignored)
Data (HexDump)           : 07 05 84 02 08 00 00                              .......

      -------------------- String Descriptors -------------------
String descriptors are not available  (because the device has problem code CM_PROB_FAILED_START)
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
- `avrdude -u -c usbasp-clone -p m32u4 -e -U flash:w:"c:\avr\flashforth\avr\hex\32u4-16MHz-USB_VID1B4F_PID9205.hex":a -U lfuse:w:0xFF:m -U hfuse:w:0xD9:m -U efuse:w:0xE9:m`
