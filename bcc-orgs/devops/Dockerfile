FROM golang:1.18-bullseye
WORKDIR /app
COPY go.mod go.sum ./
COPY src ./src

RUN ls -ls

RUN go build -o main ./src/main.go
CMD "./main"