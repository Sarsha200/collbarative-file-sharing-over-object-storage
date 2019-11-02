
Installation
----------------------------------------------------------------
1] install Microsoft SQL Server

2] install Microsoft Visual studio
----------------------------------------------------------------

Restore database
----------------------------------------------------------------
1] Open Microsoft SQL Server Management studio

2]Right click on database
	
	->Restore
	->Database
	->Select From Device option
	->Add backup file filesharedb.bak
	->click ok


-----------------------------------------------------------------  
Host Store Applications on IIS
-----------------------------------------------------------------
1] Configure IIS

2] Host the storage and backup applications i.e. CollabFileBackup and CollabFileStorage

3] Make sure you have CollabFileStorage hosted on port 9011 and CollabFileBackup on port 9012

-----------------------------------------------------------------  
Execution of Application
-----------------------------------------------------------------

1] Open CollabFileShare application in visual studio

2] change database password	
	-> Open DBConnector.cs in App_Code folder
	-> In connection string change servername and server password to your own and save

	-> Open BackupHost.cs file from same App_Code folder and change the values for 
           StorageHostName and BackupHostName to your computer name
	
3] Save and Run the project

** To use your own email account for mailing service
	- open SendEmail.cs present in App_Code folder
	- change properties address and password from existing to your gmail account userid and password.

---------------------------------------------------------------

www.sohamglobal.com


