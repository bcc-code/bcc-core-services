package services

import (
	"database/sql"
	"fmt"
	"os"

	"bcc-orgs/src/models"

	_ "github.com/lib/pq"
)

var (
	host     = os.Getenv("POSTGRES_HOST")
	port     = os.Getenv("POSTGRES_PORT")
	user     = os.Getenv("POSTGRES_USER")
	password = os.Getenv("POSTGRES_PASSWORD")
	dbname   = os.Getenv("POSTGRES_DB")
)

func Query() []models.Org {
	orgs := []models.Org{}

	psqlInfo := fmt.Sprintf("host=%s port=%s user=%s "+
		"password=%s dbname=%s sslmode=disable",
		host, port, user, password, dbname)

	db, err := sql.Open("postgres", psqlInfo)
	defer db.Close()
	if err != nil {
		panic(err)
	}
	q := "select name,orgid FROM orgs "
	rows, err := db.Query(q)
	if err != nil {
		fmt.Println("Error Query,", err)
	}

	for rows.Next() {
		var (
			name  string
			orgid int
		)
		if err := rows.Scan(&name, &orgid); err != nil {
			panic(err)
		}
		org := models.Org{OrgID: orgid, Name: name}
		orgs = append(orgs, org)
	}
	defer rows.Close()

	return orgs
}
