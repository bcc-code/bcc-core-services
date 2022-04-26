package utils

import (
	"fmt"
	"os"
	"strconv"

	"github.com/jmoiron/sqlx"
	_ "github.com/lib/pq"
)

var Db *sqlx.DB

func OpenDb() error {
	var portString = os.Getenv("POSTGRES_PORT")

	var (
		host      = os.Getenv("POSTGRES_HOST")
		port, err = strconv.Atoi(portString)
		user      = os.Getenv("POSTGRES_USER")
		password  = os.Getenv("POSTGRES_PASSWORD")
		dbname    = os.Getenv("POSTGRES_DB")
		sslmode   = os.Getenv("POSTGRES_SSL_MODE")
	)

	psqlInfo := fmt.Sprintf("host=%s port=%d user=%s "+
		"password=%s dbname=%s sslmode=%s",
		host, port, user, password, dbname, sslmode)

	db, err := sqlx.Open("postgres", psqlInfo)

	if err == nil {
		err = db.Ping()
	}

	Db = db

	return err
}
