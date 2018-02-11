# CivicdAPI

This is the backend API for the Civicd mobile application. It is written in C# using the .NET Web API 2 framework. The project is set up to connect to a SQL database using Entity Framework. 

To get started, make sure you have the following installed:

Visual Studio Community
https://www.visualstudio.com/downloads/

SQL Server Management Studio
https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms

## Setting up the database

After you first pull or clone the project, you will need to make changes to CivicdAPI/connections.config.example. 
**Copy** this file and take off the .example, renaming it to connections.config. Do NOT delete or rename the original connections.config.example.

Replace "Valid Connection String" with the connection string to your local database. 

You can set up a database using Server Explorer. 
https://msdn.microsoft.com/en-us/library/z6sa01t4.aspx

The connection string can also be retrieved through Server Explorer. Right click on the database and select "Properties". A panel on the bottom right of your screen will list the connection string. 

## Populating the database

In the Package Manager Console, run the command "update-database". This will create the database tables, as well as populate them with test data. 

## Contributing to the project

Please explore the <a href="https://codeforcharlotte.atlassian.net/wiki/spaces/MVOTE/pages">Confluence page</a> for Civicd. Be sure to read over the <a href="https://codeforcharlotte.atlassian.net/wiki/spaces/MVOTE/pages/53837825/API+Specs">API Specs</a>.

When contributing code, please work off branches, such as feature/the-feature or bug/the-bug. Submit a pull request after your changes are complete.
