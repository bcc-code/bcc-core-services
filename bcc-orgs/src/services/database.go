package services

import (
	"database/sql"
	"fmt"

	_ "github.com/lib/pq"
)

const (
	host     = "localhost"
	port     = 5432
	user     = "admin"
	password = "password1234"
	dbname   = "orgsdb"
)

func Query() {
	fmt.Println("Query from Orgs service")

	fmt.Println("Initiating database connection")

	psqlInfo := fmt.Sprintf("host=%s port=%d user=%s "+
		"password=%s dbname=%s sslmode=disable",
		host, port, user, password, dbname)

	db, err := sql.Open("postgres", psqlInfo)
	if err != nil {
		fmt.Println("PRINT: ", err)
		panic(err)
	}
	defer db.Close()

	err = db.Ping()
	if err != nil {
		fmt.Println("PRINT: ", err)
		panic(err)
	}

	fmt.Println("Successfully connected!")
}
