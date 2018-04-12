@echo off
set containterName=evolutionTest
docker stop %containterName% 
docker rm %containterName%
docker run -v evoOra:/u01/app/oracle/data --name %containterName% --detach=true store/oracle/database-enterprise:12.2.0.1
dotnet restore
dotnet build --configuration Release