FROM cosmtrek/air:latest
WORKDIR /app
COPY .air.toml go.mod go.sum ./
RUN go mod download
CMD "air"