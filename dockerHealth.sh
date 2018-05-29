dbName=$1

date
while true; do
	health=$(docker inspect --format='{{json .State.Health.Status}}' $dbName)
	if [ $health = \"healthy\" ]
	then
	  break
	fi
done
date