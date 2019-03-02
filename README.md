![img](https://raw.githubusercontent.com/DillonAd/Evolution/master/logo114H.png)
_Logo Artwork By [@richardbmx](https://github.com/richardbmx)_

# Evolution

[![Build Status](https://dev.azure.com/dillon-adams/GitHub/_apis/build/status/Evolution)](https://dev.azure.com/dillon-adams/GitHub/_build/latest?definitionId=4) [![SonarCloud Coverage](https://sonarcloud.io/api/project_badges/measure?project=DillonAd_Evolution&metric=coverage)](https://sonarcloud.io/dashboard?id=DillonAd_Evolution) [![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](http://makeapullrequest.com)

Evolution is a command line tool to move code for database applications.

## Purpose

Ever have to manage the deployment of an application where all of the logic is in the database? It is not a fun task to manage manually and there aren't many automated solutions that are affordable.

Oracle has _finally_ put out a prerelease version of their .NET Core data access library, so the .NET Core version of this is going forward. The plans are to remove the .NET Framework version after I am sure all of the functionality has been captured.

Once the pilot is done, I definitely want to support Microsoft SQL Server, and look into other database platforms that this kind of tool may be useful. Suggestions are always welcome and encouraged.

## Usage

### Step 0

Get the tool!

There are two options available:
  - _Evolution_ is published as a `dotnet tool`, so just run the command `dotnet tool install evo` and move on to the next step.
  - If you are feeling more ambitious or want to try out a prerelease version that hasn't been published yet, clone this repository and publish it from the source with `dotnet publish --self-contained --runtime {rid}` and target whatever runtime you need.
    - [Microsoft's RID Catalog](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog)

### Step 1

Write the code! (Possibly the most important part)

### Step 2

Create the Evolution file

```bash

evo add <filename> <evolutionName>

```

This will create an evolution (*.evo.sql) file with the contents the targeted file, but the name of the purpose of the evolution. These evolution files will be the scripts that will be run against the database in the next step.

### Step 2.5

This step is technically optional, but it is highly encouraged for everyone to check the Evolution file(s) into source control. This tool is meant for CI\CD, and the whole premise is predicated on building from source control. Use Git, Team Foundate Version Control, Subversion, or Starteam for all I care.  I'm sure there can be use cases outside of CI\CD, but either way (as I'm sure you already know) version control your code base.

### Step 3

Run the Evolution files!

```bash

evo exec <evolutionFile>

```

The ```<evolutionFile>``` parameter is optional. If that parameter is provided, the program will run all Evolution files that have not been executed before the one specificed. Otherwise, all unexecuted Evolution files will be run.

## Developer Setup

### Required Software

- [Install Docker](https://www.docker.com/)
- [Install .NET Core](https://www.microsoft.com/net/download/)

For Oracle:

- Run `runOracle.sh` to setup Oracle Docker image
  - You may need to go to the Docker store to get permissions to pull the image. (Oracle's policy, not mine)
    - [Image](https://store.docker.com/images/oracle-database-enterprise-edition)
    - Developers will need to create a Docker account to log in to the Docker store.

For MSSql:

- Change `<YourStrong!Passw0rd>` in `runMSSql.sh` to a valid value (Specify your own strong password that is at least 8 characters and meets the [SQL Server password requirements](https://docs.microsoft.com/en-us/sql/relational-databases/security/password-policy?view=sql-server-2017).)
- Run `runMSSql.sh` to setup MSSql Docker image
  - [Image](https://hub.docker.com/r/microsoft/mssql-server-linux/)

### Building

(From the base directory)

```bash

dotnet build

```

### Testing

#### Unit Tests

To run the unit tests, simply running the test command is sufficient.

```bash

dotnet test --filter "Category=unit"

```

#### Integration Tests

For Oracle:

To run the integration tests, you need to have started the Oracle Docker image and created the user for the tests to use.

To start the Docker image and create the necessary assets, the statements in the `/Setup/runOracle.sh` file will need to be run. The Oracle image takes time to set up, so make sure that the image is fully ready before running the tests. (The health check that reports back to the `docker ps` command sometimes lies. It's best to wait for a couple minutes after seeing the container report as _healthy_ before proceeding)

```bash

dotnet test --filter "Category=integration&Provider=Oracle"

```

For MSSql:

To run the integration tests, you need to have started the MSSql Docker image and created the user for the tests to use.

To start the Docker image and create the necessary assets, the statements in the `/Setup/runMSSql.sh` file will need to be run. The MSSql image takes time to set up, so make sure that the image is fully ready before running the tests. (The health check that reports back to the `docker ps` command sometimes lies. It's best to wait for a couple minutes after seeing the container report as _healthy_ before proceeding)

```bash

dotnet test --filter "Category=integration&Provider=MSSql"

```

Note: Capitalization is important here.


## Issues?

If you have any trouble with the steps provided, please submit an issue or a pull request! All feedback is welcome!