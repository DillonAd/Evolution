#!/bin/bash
dbName=$1
echo $(date)
health=
until [[ $health == "healthy" ]]
do
    echo $dbName
	health=$(docker inspect --format='{{json .State.Health.Status}}' $dbName)
    echo $health
done
echo $(date)