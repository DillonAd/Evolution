#!/bin/sh

dbName=evolution
oraUser=appUser
oraPwd=appPassword
oraInstance=evolutionDB
oraPort1=6666
oraPort2=6667

# Setup Docker container for Oracle database
docker run -d --name $dbName ^
	-p $oraPort1:1521 -p $oraPort2:5500 ^
	-e ORACLE_SID=$oraInstance ^
	store/oracle/database-enterprise:12.2.0.1

# Check for health
date
:healthLoop
health=(docker inspect --format='{{json .State.Health.Status}}' $dbName)
if not $health == "healthy" goto healthLoop
date

# Setup test user on Oracle database
echo 'create user c##$oraUser identified by $oraPwd' | sqlplus "sys/Oradoc_db1@(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=%oraPort1%))(CONNECT_DATA=(SERVER=dedicated)(SERVICE_NAME=ORCLCDB.localdomain)))" as sysdba
echo 'grant dba to c##$oraUser;' | sqlplus "sys/Oradoc_db1@(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=%oraPort1%))(CONNECT_DATA=(SERVER=dedicated)(SERVICE_NAME=ORCLCDB.localdomain)))" as sysdba
echo 'grant create session to c##$oraUser;' | sqlplus "sys/Oradoc_db1@(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=%oraPort1%))(CONNECT_DATA=(SERVER=dedicated)(SERVICE_NAME=ORCLCDB.localdomain)))" as sysdba

# Run Tests
# dotnet test Evolution.Test\ --filter TestCategory=integration --settings test.runsettings -- TestRunParameters.Parameter.OracleUser="%oraUser%"  TestRunParameters.OraclePassword=%oraPwd% TestRunParameters.OracleInstance=%oraInstance% TestRunParameters.OraclePort=%oraPort1%
dotnet test ./Evolution.Test.Unit/Evolution.Test.Unit.csproj --filter Category=integration --logger "trx;LogFileName=results\tests_integration.xml"

# TearDown Docker container
docker stop $dbName
docker rm $dbName
