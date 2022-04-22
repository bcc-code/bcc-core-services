package services

import (
	"database/sql"
	"fmt"
	"os"
	"strconv"

	_ "github.com/lib/pq"
)

var portString = os.Getenv("POSTGRES_PORT")

var (
	host      = os.Getenv("POSTGRES_HOST")
	port, err = strconv.Atoi(portString)
	user      = os.Getenv("POSTGRES_USER")
	password  = os.Getenv("POSTGRES_PASSWORD")
	dbname    = os.Getenv("POSTGRES_DB")
	sslmode   = os.Getenv("POSTGRES_SSL_MODE")
)

func OpenDb() *sql.DB {
	psqlInfo := fmt.Sprintf("host=%s port=%d user=%s "+
		"password=%s dbname=%s sslmode=%s",
		host, port, user, password, dbname, sslmode)

	db, err := sql.Open("postgres", psqlInfo)
	if err != nil {
		panic(err)
	}
	return db
}
