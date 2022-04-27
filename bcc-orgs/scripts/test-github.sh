docker-compose -f ${BASH_SOURCE%/*}/../devops/test.docker-compose.yml -p "orgs-api-test" up -d --build
./${BASH_SOURCE%/*}/reset-db-docker.sh
./${BASH_SOURCE%/*}/test-docker.sh
