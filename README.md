# UrlShortener

PROJECT DESCRIPTION 
This is a MVC web application designed to allow the user to shorten urls. 

Compatible witg .Net Core 3.1 or .Net 5

Languages/ frameworks: C#, JQuery, Bootstrap, Entity Framework Core, Linq

How to Run Locally
1. Ensure Microsoft SQL Server Management Studio or equivalent is installed on your local machine 
https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16
2. Open project in MS Visual Studio 2019 or later versions 
3. This project can leverage entity framework code first to create a local DB. 
   The appsettings.json file contains a default connection connection string for local server access, if you wish to change this please do so. 
4. Navigate to Tools->Nuget Package Manager -> Package Manager Console and complete the following commands. 
	add-migration <migration name>
	update-database
5. Run project via IIS Express 

Road Map 
Complete Unit Tests
Add Authenication for users to save/track shortened urls via foreign key relationship between user and url tables 
Add "Clicks" functionality to urls to track number of visits 
Improve UI
Currently adding the same url twice will result in two separate short links, enhancement required to avoid duplicate short urls.
Host on Azure/ AWS 
Add Logging and Exception Handling 


