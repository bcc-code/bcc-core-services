#!/bin/bash 
docker-compose -f ${BASH_SOURCE%/*}/../devops/dev.docker-compose.yml -p "orgs-api-dev" up --build
