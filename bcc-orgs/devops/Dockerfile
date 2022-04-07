##
## Build
##
FROM golang:1.18-bullseye as build
WORKDIR /app
COPY go.mod go.sum ./
COPY src ./src
RUN go mod download

RUN CGO_ENABLED=0 GOOS=linux GOARCH=amd64 go build -o main ./src/main.go

##
## Deploy
##
FROM scratch
WORKDIR /
COPY --from=build /app/main /usr/bin/
ENTRYPOINT ["main"]