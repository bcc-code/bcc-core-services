package utils

import (
	"fmt"
	"os"

	"github.com/jmoiron/sqlx"
	_ "github.com/lib/pq"
)

var Db *sqlx.DB

func OpenDb() error {

	var (
		host     = os.Getenv("POSTGRES_HOST")
		port     = os.Getenv("POSTGRES_PORT")
		user     = os.Getenv("POSTGRES_USER")
		password = os.Getenv("POSTGRES_PASSWORD")
		dbname   = os.Getenv("POSTGRES_DB")
	)

	psqlInfo := fmt.Sprintf("host=%s port=%s user=%s "+
		"password=%s dbname=%s sslmode=disable",
		host, port, user, password, dbname)

	db, err := sqlx.Open("postgres", psqlInfo)

	if err == nil {
		err = db.Ping()
	}

	Db = db

	return err
}
