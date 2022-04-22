#!/bin/bash 
docker exec orgs_api bash /scripts/wait-for-orgs-db.sh -- bash /scripts/reset-db.sh
