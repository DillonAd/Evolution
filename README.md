# Evolution
[![Build Status](https://dev.azure.com/dillon-adams/GitHub/_apis/build/status/Evolution)](https://dev.azure.com/dillon-adams/GitHub/_build/latest?definitionId=4)

Evolution is a command line tool to move code for database applications.

## Purpose
Ever have to manage the deployment of an application where all of the logic is in the database? It is not a fun task to manage manually and there aren't many automated solutions that are affordable.

Oracle has _finally_ put out a prerelease version of their .NET Core data access library, so the .NET Core version of this is going forward. The plans are to remove the .NET Framework version after I am sure all of the functionality has been captured.

Once the pilot is done, I definitely want to support Microsoft SQL Server, and look into other database platforms that this kind of tool may be useful. Suggestions are always welcome and encouraged. 

## Developer Setup

### Required Software
- Install Docker (https://www.docker.com/)
- Install .NET Core (https://www.microsoft.com/net/download/)
- Run ``` runOracle.sh ``` to setup Oracle Docker image
  - You may need to go to the Docker store to get permissions to pull the image. (Oracle's policy not mine)

### Building
(From the base directory)

```bash

dotnet build

```

### Testing

#### Unit Tests
To run the unit tests, simply running the test command is sufficient.

```bash

dotnet test --filter Category=unit

```

#### Integration Tests

To run the integration tests, you need to have started the Oracle Docker image and created the user for the tests to use.

```bash

dotnet test --filter Category=integration

```

## Deployment

At the moment, the way to deploy this solution is to build it from the source. Follow the build steps above and let me know if I missed anything.

## Usage

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
