#!/bin/bash
dbName=$1
echo $(date)
health=
until [[ $health == \"healthy\" ]]
do
	health=$(docker inspect --format='{{json .State.Health.Status}}' $dbName)
done
echo $(date)