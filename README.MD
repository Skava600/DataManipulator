# Data Manipulator
## Task
1. Generate 100 text files with the following structure, each containing
100,000 lines
random date in last 5 years || random set of 10 Latin characters ||
random set of 10 Russian characters || random positive even
integer number between 1 and 100,000,000 || random positive
number with 8 decimal places in the range from 1 to 20

 - Sample output:
03/03/2015||ZAwRbpGUiK||mDMYaNkuKD||14152932||7.87742021||
01/23/2015||vgHKThbgrP||LDKKHysKhshZTs||35085588||8.49822372||
10/17/2017||AuTVNvaGRB||mCheprIecrA||34259646||17.7248118||
09/24/2014||ArIAASwOnE||ChpYMdShlyfU||23252734||14.6239438||
10/16/2017||eUkiAhUWmZ||ZeZhYzyaShAesh||27831190||8,10838026||

2. Implement the merging of files into one. While merging should be possible
remove from all files lines with a given combination of characters, for example, "abc" with output
information about the number of deleted rows
3. Create a procedure for importing files with such a set of fields into a table in the DBMS. When importing
the progress of the process should be displayed (how many lines are imported, how many are left)
4. Implement a stored procedure in the database (or a script with an external sql query) that considers
the sum of all integers and the median of all fractional numbers
All scripts/procedures/requests from points 2-3-4 must be repeatable.

## How it works

 - In appsettings.json you can configure the apllication  - specifying the path where to generate  files, path to file where to combine data and connection string for database.

 - In folder SqlQueries stored necessary procedures for database, and creating table query.

 - If you would like to change generation parameters, you can change it in DataManipulatorGenerator project in data generator class.


## Demonstration of work

![alt text](https://github.com/Skava600/DataManipulator/blob/master/Screens/DataFileExample.png?raw=true)
**Generated data file example** 

![alt text](https://github.com/Skava600/DataManipulator/blob/master/Screens/FolderWithGeneratedFilesExample.png?raw=true)
**Folder with generated files**

![alt text](https://github.com/Skava600/DataManipulator/blob/master/Screens/MergedFileExample.png?raw=true)
**Combined filed example**

![alt text](https://github.com/Skava600/DataManipulator/blob/master/Screens/EmptyTableExample.png?raw=true)
**Empty table.**

![alt text](https://github.com/Skava600/DataManipulator/blob/master/Screens/ImportingToDbExample.png?raw=true)
**Importing to db.**

![alt text](https://github.com/Skava600/DataManipulator/blob/master/Screens/FilledTableExample.png?raw=true)
**Filled table.**

![alt text](https://github.com/Skava600/DataManipulator/blob/master/Screens/MedianAndSumExample.png?raw=true)
**Counted median and sum with stored procedure in database**


