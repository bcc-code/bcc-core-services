#!/bin/bash
bash /scripts/reset-db.sh
go test -v ./...
