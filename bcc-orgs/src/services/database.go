package services

import (
	"database/sql"
	"fmt"

	_ "github.com/lib/pq"
)

const (
	host     = "host.docker.internal"
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
		panic(err)
	} else {
		q := "select name,orgid FROM orgs "
		rows, err := db.Query(q)
		if err != nil {
			fmt.Println("Error Query,", err)
		}

		fmt.Println("Rows received", rows)

		for rows.Next() {
			var (
				name  string
				orgid int
			)
			if err := rows.Scan(&name, &orgid); err != nil {
				panic(err)
			}
			fmt.Printf("orgid %d name is %s\n", orgid, name)
		}
		defer rows.Close()
	}
	defer db.Close()
}
