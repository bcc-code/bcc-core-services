FROM golang:1.18-bullseye

# Install migration tool
RUN go install -tags "nomymysql nomysql nosqlite3" github.com/pressly/goose/v3/cmd/goose@latest

# Prepare application
WORKDIR /app
COPY go.mod go.sum ./
RUN go mod download
