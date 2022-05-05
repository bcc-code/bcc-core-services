FROM golang:1.18-bullseye

# Install tools (for migration, openAPI docs and hot reaload)
RUN go install -tags "nomymysql nomysql nosqlite3" github.com/pressly/goose/v3/cmd/goose@latest
RUN go install github.com/swaggo/swag/cmd/swag@latest
RUN go install github.com/cosmtrek/air@latest

# Prepare application
WORKDIR /app

# Install dependencies
COPY .air.toml go.mod go.sum ./
RUN go mod download
