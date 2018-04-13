# Evolution
Evolution is a command line tool to move code for database applications.

## Purpose ##
Ever have to manage the deployment of an application where all of the logic is in the database? It is not a fun task to manage manually and there aren't many automated solutions that are affordable.

~~ At the moment, the focus is moving PL/SQL code between Oracle databases. Sadly, Oracle hasn't gotten around to updating their libraries to .NET Standard so this is a Windows only solution for Oracle PL/SQL code. ~~
Oracle has not yet upgraded their libraries to DotNet Core. Until they do, I have had to create a DotNet Framework version on this application.

Once the pilot is done, I definitely want to support Microsoft SQL Server, and look into other database platforms that this kind of tool may be useful. Suggestions are always welcome and encouraged. 