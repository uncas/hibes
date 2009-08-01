import urllib.request
from urllib.request import *

# Fetches the estimate to a latex file:
def GetEstimate():
    estimateAsLatexUrl = 'http://localhost:49999/EstimateAsLatex.ashx'
    latexFileName = 'test.tex'
    # Downloads the estimate as a latex file:
    urllib.request.urlretrieve(estimateAsLatexUrl, latexFileName)

# Replaces oldWord with newWord:
def ReplaceLatex():
    oldWord = 'Dage'
    newWord = '**Dage**'
    outputFileName = 'testReplaced.tex'
    outputFile = open(outputFileName, "a")
    for line in open(latexFileName, "r"):
       line = line.replace(oldWord, newWord)
       outputFile.write(line)
    outputFile.close()

# Creates pdf file:
def CreatePdfFile():
    os.system("dir *.txt")
    os.system("pdflatex " + outputFileName)
    os.system("pdflatex " + outputFileName)

def send_mail(fromAddress, toAddress, subject, text, server="smtp.mail.dk"):
    # Import smtplib for the actual sending function
    import smtplib

    # Import the email modules we'll need
    from email.mime.text import MIMEText

    # Create a text/plain message
    msg = MIMEText(text)

    # me == the sender's email address
    # you == the recipient's email address
    msg['Subject'] = subject
    msg['From'] = fromAddress
    msg['To'] = toAddress

    # Send the message via our own SMTP server, but don't include the
    # envelope header.
    s = smtplib.SMTP(server)
    s.sendmail(fromAddress, toAddress, msg.as_string())
    s.quit()

import os
import sys
import smtplib
# For guessing MIME type based on file name extension
import mimetypes

from optparse import OptionParser

from email import encoders
from email.message import Message
from email.mime.multipart import MIMEMultipart
from email.mime.application import MIMEApplication
from email.mime.text import MIMEText

def send_mail2(fromAddress, toAddress, subject, text, server = 'smtp.mail.dk', fileNames = []):
    # Create the container (outer) email message.
    msg = MIMEMultipart()
    msg['Subject'] = subject
    me = fromAddress
    family = toAddress
    msg['From'] = me
    msg['To'] = family
    msg.preamble = subject

    for fileName in fileNames:
        # Open the files in binary mode.  Let the MIMEImage class automatically
        # guess the specific image type.
        fp = open(fileName, 'rb')
        file = MIMEApplication(fp.read())
        fp.close()
        msg.attach(file)

    # Send the email via our own SMTP server.
    s = smtplib.SMTP(server)
    s.sendmail(me, family, msg)
    s.quit()

#send_mail('ole@uncas.dk', 'ole@uncas.dk', 'test fra python', '...')
#send_mail2('ole@uncas.dk', 'ole@uncas.dk', 'test fra python', '...', 'smtp.mail.dk', ['C:\\Users\\Ole\\Documents\\Projects\\hibes\\doc\\testReplaced.pdf'])
send_mail3('ole@uncas.dk', 'ole@uncas.dk', 'test fra python', '...', 'smtp.mail.dk', ['C:\\Users\\Ole\\Documents\\Projects\\hibes\\doc\\testReplaced.pdf'])
