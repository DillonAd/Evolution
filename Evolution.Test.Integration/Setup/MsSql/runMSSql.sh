CONTAINER_NAME=evolution-sql
CURRENT_DIRECTORY=$(dirname $0)

docker run -d --name $CONTAINER_NAME -p 1433:1433 --rm -e ACCEPT_EULA=Y -e MSSQL_PID=Express -e 'SA_PASSWORD=YourStrong!Passw0rd' microsoft/mssql-server-linux
docker cp $CURRENT_DIRECTORY/SetupMSSql.sql $CONTAINER_NAME:SetupMSSql.sql

# No health check needed for the MsSql image
# $CURRENT_DIRECTORY/../dockerHealth.sh $CONTAINER_NAME

docker exec $CONTAINER_NAME /opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U SA -P 'YourStrong!Passw0rd' -i ./SetupMSSql.sql