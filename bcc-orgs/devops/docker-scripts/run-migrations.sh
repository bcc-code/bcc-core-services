#!/bin/bash
DB_CONNECTION="host=$POSTGRES_HOST port=$POSTGRES_PORT user=$POSTGRES_USER password=$POSTGRES_PASSWORD dbname=$POSTGRES_DB sslmode=$POSTGRES_SSL_MODE"
echo "Running migrations"
goose -dir db/migrations -table schema_migrations postgres "$DB_CONNECTION" up 
echo "Finished migrations"
