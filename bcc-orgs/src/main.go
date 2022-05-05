package main

import (
	"bcc-orgs/src/router"

	"bcc-orgs/src/utils"

	"github.com/gin-gonic/gin"
)

func main() {
	utils.InitEnv()

	dbErr := utils.OpenDb()
	if dbErr != nil {
		panic(dbErr)
	}

	r := gin.Default()
	router.LoadRoutes(r)

	r.Run()
}
