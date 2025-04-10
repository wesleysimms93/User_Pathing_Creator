
import numpy as np
import math

def next_char(char):
    # Find the next character using the ord() and chr() functions
    return chr(ord(char) + 1)

def unique_number(num1, num2):
    # Ensure the smaller number comes first
    return min(num1, num2) * 10000 + max(num1, num2)

def unique_cord_number(num1, num2):
    # Ensure the smaller number comes first
    return num1 * 10000 + num2



def int_to_two_byte_list(value):
    if value < 0 or value > 65535:
        raise ValueError("Integer must be in the range 0-65535 to fit into two bytes.")
    
    # Convert the integer to a two-byte representation
    byte_1 = (value >> 8) & 0xFF 
    byte_2 = value & 0xFF
    return byte_1 , byte_2

class Point:
    def __init__(self ,x,y,z,name):
        self.x = int(x)
        self.y = int(y)
        self.z = int(z)
        self.name = int(name)

    def __str__(self):
        return str(self.name)
    
    def create_from_gcode(string, name):
        # Split the string into components
        parts = string.split()
        # Extract x, y, and z values
        point  = Point(0,0,0,name)
        point.x = int(parts[1][1:])  # Remove 'X' and convert to int
        point.y = int(parts[2][1:])  # Remove 'Y' and convert to int
        point.z = int(parts[3][1:])  # Remove 'Z' and convert to int
        return point

    


    def point_data_bytes(self):
            out = []
            b1, b2 = int_to_two_byte_list(self.name)
            out.append(b1)
            out.append(b2)
            b1, b2 = int_to_two_byte_list(self.x)
            out.append(b1)
            out.append(b2)
            b1, b2 = int_to_two_byte_list(self.y)
            out.append(b1)
            out.append(b2)
            b1, b2 = int_to_two_byte_list(self.z)
            out.append(b1)
            out.append(b2)
            return out
    

    def calculate_distance(point1, point2):
    # Calculate the Euclidean distance between two points
        distance = math.sqrt((point2.x - point1.x)**2 + (point2.y - point1.y)**2)
        return distance






