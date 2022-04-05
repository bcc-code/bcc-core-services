##
## Build
##
FROM golang:1.18-bullseye
WORKDIR /app
COPY go.mod go.sum ./
COPY src ./src

RUN go build -o /main ./src/main.go

##
## Deploy
##
FROM gcr.io/distroless/base-debian10
WORKDIR /
COPY --from=0 /main /main
USER nonroot:nonroot
ENTRYPOINT ["/main"]