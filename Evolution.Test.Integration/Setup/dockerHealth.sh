#!/bin/bash

containerName=$1
echo $(date)

health=
until [[ $health == \"healthy\" ]]
do
	health=$(docker inspect --format='{{json .State.Health.Status}}' $containerName)
done
echo $(date)