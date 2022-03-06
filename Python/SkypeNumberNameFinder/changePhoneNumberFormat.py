#This file is for changing format from +44 737 515406 to +44737515406 to match Skype image output
#Then just copy the tel to the database, otherwise will not work.
from os import sep
import pandas as pd
pd.options.mode.chained_assignment = None
result = pd.read_csv("treatwellCustomer.csv", index_col=0,
                     dtype={'first name': object, 'phone': object})


for i in range(len(result["phone"])):
    result["phone"].iloc[i] = str(result["phone"][i]).replace(" ", "")
print(result["phone"])
result.to_excel("editContact1.xlsx")
