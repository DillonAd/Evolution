#!/bin/bash

CONTAINER_NAME=evolution-oracle
CURRENT_DIRECTORY=$(dirname $0)

docker run -d --name $CONTAINER_NAME --rm -p 6666:1521 -p 6667:5500 -e ORACLE_SID=evolution store/oracle/database-enterprise:12.2.0.1

dos2unix -- $CURRENT_DIRECTORY/../dockerHealth.sh
bash $CURRENT_DIRECTORY/../dockerHealth.sh $CONTAINER_NAME

docker cp $CURRENT_DIRECTORY/tnsnames.ora $CONTAINER_NAME:tnsnames.ora
docker exec $CONTAINER_NAME /bin/bash -c 'cp tnsnames.ora $ORACLE_HOME/network/admin/tnsnames.ora'

# Oracle lied about being healthy
docker exec $CONTAINER_NAME sleep 1m

docker cp $CURRENT_DIRECTORY/SetupOracle.sql $CONTAINER_NAME:SetupOracle.sql
docker exec $CONTAINER_NAME bash -c 'source /home/oracle/.bashrc; sqlplus sys/Oradoc_db1@localhost:1521/ORCLCDB.localdomain as sysdba @SetupOracle.sql; exit \$?'
