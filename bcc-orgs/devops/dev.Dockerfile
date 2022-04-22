FROM golang:1.18-bullseye

# Install migration tool
RUN go install -tags "nomymysql nomysql nosqlite3" github.com/pressly/goose/v3/cmd/goose@latest

# Prepare application
WORKDIR /app
RUN go install github.com/cosmtrek/air@latest
COPY .air.toml go.mod go.sum ./
RUN go mod download

