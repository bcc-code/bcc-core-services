package main

import (
	"bcc-orgs/src/router"

	"bcc-orgs/src/utils"

	"github.com/gin-gonic/gin"

	_ "github.com/swaggo/files"
	_ "github.com/swaggo/gin-swagger"
)

// @title           BCC Orgs API
// @version         1.0
// @description     This is the Orgs API

// @license.name  Apache 2.0
// @license.url   http://www.apache.org/licenses/LICENSE-2.0.html

// @host      localhost:4000
// @BasePath  /api/v1

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
