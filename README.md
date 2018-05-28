# Evolution
Evolution is a command line tool to move code for database applications.

## Purpose
Ever have to manage the deployment of an application where all of the logic is in the database? It is not a fun task to manage manually and there aren't many automated solutions that are affordable.

Oracle has _finally_ put out a prerelease version of their .NET Core data access library, so the .NET Core version of this is going forward. The plans are to remove the .NET Framework version after I am sure all of the functionality has been captured.

Once the pilot is done, I definitely want to support Microsoft SQL Server, and look into other database platforms that this kind of tool may be useful. Suggestions are always welcome and encouraged. 

## Developer Setup

### Required Software
- Install Docker (https://www.docker.com/)
- Pull Oracle Database Docker image (https://store.docker.com/images/oracle-database-enterprise-edition)
- Install .NET Core (https://www.microsoft.com/net/download/)

### Building
(From the base directory)

```bash

dotnet build
dotnet test --filter Category=unit
dotnet test --filter Category=integration

```

## Deployment

At the moment, the way to deploy this solution is to build it from the source. Follow the build steps above and let me know if I missed anything.

## Usage

```C#

//TODO

```