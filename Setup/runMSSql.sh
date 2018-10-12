docker run -d --name evolution -h evolution --rm -p 6666:1433 -p 6667:1434 -e "ACCEPT_EULA=Y" -e "SQLCMDSERVER=evolution" -e 'SA_PASSWORD=<YourStrong!Passw0rd>' mcr.microsoft.com/mssql/server:2017-latest
docker cp ./SetupMSSql.sql evolution:SetupMSSql.sql
docker exec evolution bash /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P '<YourStrong!Passw0rd>' -i SetupMSSql.sql