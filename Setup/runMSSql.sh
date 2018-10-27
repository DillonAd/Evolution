docker run -d --name evolution-sql -p 1433:1433 --rm -e ACCEPT_EULA=Y -e MSSQL_PID=Express -e 'SA_PASSWORD=YourStrong!Passw0rd' microsoft/mssql-server-linux
docker cp ./SetupMSSql.sql evolution-sql:SetupMSSql.sql
docker exec evolution-sql /opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U SA -P 'YourStrong!Passw0rd' -i ./SetupMSSql.sql