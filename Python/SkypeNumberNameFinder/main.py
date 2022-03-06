import tkinter as tk
import pandas as pd
from os import sep
from PIL import Image
import pyautogui
import pytesseract
import os
from plyer import notification

pd.options.mode.chained_assignment = None
path = os.path.dirname(os.path.abspath(__file__))
save_path = path+"\image2.png"#This can be change to make file name you want.
pytesseract.pytesseract.tesseract_cmd = r"C:\Program Files\Tesseract-OCR\tesseract"


def GetPhoneNumberFromImage():
    im = pyautogui.screenshot(region=(57, 43, 207, 32))#This will take a screenshot area you define x,y, size of x , size of y box
    im.save(save_path)
    result = pytesseract.image_to_string(
        Image.open('image2.png'), config='--psm 7')#if you change file name above, you have to change this.
    return result


def GetNameOfACustomer(customerTel):
    result = pd.read_csv("treatwellCustomer.csv", index_col=0,#this is the name of your database.
                         dtype={'first name': object, 'phone': object})

    foundCustomer = "Cannot find customer name in the system"
    for i in range(len(result)):
        if customerTel == str(result["phone"][i]): # this is the title of the database for phone must MATCH
            foundCustomer = str(result["first name"][i]) + \
                " " + str(result["last name"][i]) #first name last name MUST MATCH

    return foundCustomer


# phoneNumber = GetPhoneNumberFromImage()
# # remove any newline
# phoneNumber_removeNewLine = phoneNumber.strip("\n")
# # number to test during production
# phoneNumberTest = "+442084411568"

# # print(phoneNumberTest == phoneNumber_removeNewLine)
# # print(phoneNumber)

# # calling method to get customer name
# foundCustomer = GetNameOfACustomer(phoneNumber_removeNewLine)
# # print(foundCustomer)

# notification.notify(
#     title=foundCustomer,
#     message=phoneNumber_removeNewLine,
#     app_icon=None,
#     timeout=5,
# )


def GetCustomerNameAndNotify():
    phoneNumber = GetPhoneNumberFromImage()
    phoneNumber_removeNewLine = phoneNumber.strip("\n")
    foundCustomer = GetNameOfACustomer(phoneNumber_removeNewLine)
    notification.notify(
        title=foundCustomer,
        message=phoneNumber_removeNewLine,
        app_icon=None,
        timeout=20,
    )


root = tk.Tk()

canvas1 = tk.Canvas(root, width=50, height=50)
canvas1.pack()


# def hello():
#     label1 = tk.Label(root, text='Hello World!', fg='green',
#                       font=('helvetica', 12, 'bold'))
#     canvas1.create_window(150, 200, window=label1)


# def hello1():
#     label1 = tk.Label(root, text='Hello World111!', fg='red',
#                       font=('helvetica', 12, 'bold'))
#     canvas1.create_window(150, 200, window=label1)


# button1 = tk.Button(text='Click Me', command=hello, bg='brown', fg='white')
button2 = tk.Button(
    text='Get Name', command=GetCustomerNameAndNotify, bg='brown', fg='white')
# button1_window = canvas1.create_window(30, 150, window=button1)
# button1.update()
button2_window = canvas1.create_window(20, 20, window=button2)
button2.update()

root.mainloop()
