#import spidev
import sys
import Support

#if the current process needs to be wiped
flag = False
    
    

#def send_spi_data(data):
    
#    spi = spidev.SpiDev()
#    spi.open(0, 0)  # Open SPI bus (bus 0, device 0)
#    spi.mode = 0b00  # Motorola mode
#    spi.max_speed_hz = 50000  # Set speed (adjust as necessary)
#    for byte in data:
#        spi.xfer([byte])  # Transfer data (list containing one byte)
#    spi.close()

def load_G_code_File(file_name):
    moves = []
    file = open(file_name, 'r')
    char = 1
    for line in file:
        moves.append(Support.Point.create_from_gcode(line,char))
        char += 1

    file.close()
    return moves

if __name__ == "__main__":
    commands = load_G_code_File("gcodetest.txt")
    print(len(commands))
    test = bytes(1) 
    test = 0x34
    print(test)
    for point in commands:
        print(point.point_data_bytes())
