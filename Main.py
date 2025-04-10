#!/usr/bin/python

import spidev
import RPi.GPIO as GPIO
import time

# GPIO setup
INPUT_PIN = 17  # Replace with your GPIO pin number
GPIO.setmode(GPIO.BCM)  # Use BCM GPIO numbering
GPIO.setup(INPUT_PIN, GPIO.IN, pull_up_down=GPIO.PUD_DOWN)  # Set as input with pull-down resistor





bus = 0						#Select SPI bus 0
device_cs = 1				#CS pin. Select 0 or 1, depending on the connection to the RPi
spi = spidev.SpiDev()		# Enable SPI
spi.open(bus, device_cs)	#Open connection to the device
spi.max_speed_hz = 500000
spi.mode = 0


## Prepare the 12-bit word
word = 0xABC  # Example 12-bit word (0xABC)
high_byte = (word >> 4) & 0xFF  # Extract the high 8 bits
low_byte = (word & 0xF) << 4  # Extract the low 4 bits and shift

try:
    #print("Waiting for GPIO input to go HIGH...")
    # Wait for input pin to go HIGH
    while GPIO.input(INPUT_PIN) == GPIO.LOW:
        time.sleep(0.01)  # Avoid busy-waiting

    print("GPIO input is HIGH! Sending SPI data...")
    # Send the 12-bit word as two bytes
    spi.xfer2([high_byte])
    spi.xfer2([low_byte])

finally:
    # Clean up resources
    spi.close()
    GPIO.cleanup()

