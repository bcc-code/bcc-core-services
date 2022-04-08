package services

import (
	"database/sql"
	"fmt"
	"os"
	"strconv"

	"bcc-orgs/src/models"

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

func Query() []models.Org {
	orgs := []models.Org{}

	psqlInfo := fmt.Sprintf("host=%s port=%d user=%s "+
		"password=%s dbname=%s sslmode=%s",
		host, port, user, password, dbname, sslmode)

	db, err := sql.Open("postgres", psqlInfo)
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
	defer db.Close()

	return orgs
}
