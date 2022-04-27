#!/bin/bash 
docker-compose -f ${BASH_SOURCE%/*}/../devops/test.docker-compose.yml run --rm orgs_api_test
