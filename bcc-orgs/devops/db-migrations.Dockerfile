# install migrate tool
FROM golang:1.18-alpine
RUN go install -tags 'postgres' github.com/golang-migrate/migrate/v4/cmd/migrate@latest

WORKDIR /
COPY db/migrations/ ./migrations
RUN ls bin
# RUN migrate -path migrations -database postgres://admin:1234@orgs_db:5432/orgsdb?sslmode=disable up
# FROM ubuntu:trusty
# CMD ["/bin/bash -c"]