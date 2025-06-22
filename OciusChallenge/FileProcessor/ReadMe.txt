###Used below packages in this project###
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools


### Assumption ###
 - FileName should not be larger than 150
 - Each word length should not be larger than 00
 - By default seperator takes as space (' ') value from file
 - By default parallel count would be 1
 - Batch size and batch count is compulsary as input parameter
 - Each file should process only one time and hence we will move this file to drop path after completes operation successfully.

###Note###
Some Consurrent request still have the issue might be possible to get duplicate rows and and processed same files and saved in database