@echo off

set dbName=evolution
set oraUser=appUser
set oraPwd=appPassword
set oraInstance=evolutionDB
set oraPort1=6666
set oraPort2=6667

rem Setup Docker container for Oracle database
rem docker run -d -it --name %dbName% -P store/oracle/database-enterprise:12.2.0.1

docker run -d --name %dbName% ^
	-p %oraPort1%:1521 -p %oraPort2%:5500 ^
	-e ORACLE_SID=%oraInstance% ^
	store/oracle/database-enterprise:12.2.0.1

rem Check for health
echo %TIME%
:healthLoop
for /f %%i in ('docker inspect --format="{{json .State.Health.Status}}" %dbName%') do set health=%%i
if not %health% == "healthy" goto healthLoop
echo %TIME%

rem Setup test user on Oracle database
@echo create user c##%oraUser% identified by %oraPwd%; | sqlplus "sys/Oradoc_db1@(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=%oraPort1%))(CONNECT_DATA=(SERVER=dedicated)(SERVICE_NAME=ORCLCDB.localdomain)))" as sysdba
rem @echo grant dba to c##%oraUser%; | sqlplus "sys/Oradoc_db1@(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=%oraPort1%))(CONNECT_DATA=(SERVER=dedicated)(SERVICE_NAME=ORCLCDB.localdomain)))" as sysdba
rem @echo grant create session to c##%oraUser%; | sqlplus "sys/Oradoc_db1@(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=%oraPort1%))(CONNECT_DATA=(SERVER=dedicated)(SERVICE_NAME=ORCLCDB.localdomain)))" as sysdba

rem Run Tests
rem dotnet test Evolution.Test\ --filter TestCategory=integration --settings test.runsettings 
rem -- TestRunParameters.Parameter.OracleUser="%oraUser%"  TestRunParameters.OraclePassword=%oraPwd% TestRunParameters.OracleInstance=%oraInstance% TestRunParameters.OraclePort=%oraPort1%

rem TearDown Docker container
docker stop %dbName%
docker rm %dbName%