FROM golang:1.18-bullseye

# Prepare migrations
WORKDIR /db
RUN go install github.com/pressly/goose/v3/cmd/goose@latest
WORKDIR /scripts
COPY devops/run-migrations.sh /devops/wait-for-it.sh devops/start-app-with-migrations.sh ./

# Prepare application
WORKDIR /app
RUN go install github.com/cosmtrek/air@latest
COPY .air.toml go.mod go.sum ./
RUN go mod download

WORKDIR /
