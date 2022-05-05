#!/bin/bash
bash /scripts/wait-for-it.sh orgs_db:$POSTGRES_PORT $@
