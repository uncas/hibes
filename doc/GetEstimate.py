import urllib.request
from urllib.request import *

# Fetches the estimate to a latex file:
def GetEstimate(estimateAsLatexUrl, latexFileName):
    urllib.request.urlretrieve(estimateAsLatexUrl, latexFileName)

# Replaces oldWord with newWord:
def ReplaceLatex(latexFileName, outputFileName, oldString, newString):
    outputFile = open(outputFileName, "a")
    for line in open(latexFileName, "r"):
       line = line.replace(oldString, newString)
       outputFile.write(line)
    outputFile.close()

# Creates pdf file:
def CreatePdfFile(latexFileName):
    os.system("pdflatex " + latexFileName)
    os.system("pdflatex " + latexFileName)

estimateAsLatexUrl = 'http://localhost/hibes/EstimateAsLatex.ashx'
rawFileName = 'estimatesRaw.tex'
tempFileName = 'estimatesTemp.tex'
outputFileName = 'estimates.tex'

GetEstimate(estimateAsLatexUrl, rawFileName)
ReplaceLatex(rawFileName, tempFileName, '@TOP@', 'TOP CONTENT HERE')
ReplaceLatex(tempFileName, outputFileName, '@BOTTOM@', 'BOTTOM CONTENT HERE')
CreatePdfFile(outputFileName)
