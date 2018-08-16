docker run -d --name evolution --rm -p 6666:1521 -p 6667:5500 -e ORACLE_SID=evolution store/oracle/database-enterprise:12.2.0.1-slim

dbName=$1
echo $(date)
health=
until [[ $health == \"healthy\" ]]
do
	health=$(docker inspect --format='{{json .State.Health.Status}}' $dbName)
done
echo $(date)

sqlplus sys/Oradoc_db1@localhost:6666/ORCLCDB.localdomain as sysdba @SetupOracle.sql