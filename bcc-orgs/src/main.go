package main

import (
	"bcc-orgs/src/router"

	"bcc-orgs/src/utils"

	"github.com/gin-contrib/cors"
	"github.com/gin-gonic/gin"
)

// @title        BCC Orgs API
// @version      1.0.0
// @description  This is the Orgs API

// @license.name  Apache 2.0
// @license.url   http://www.apache.org/licenses/LICENSE-2.0.html

// @securityDefinitions.oauth2.application ClientCredentials
// @tokenUrl /docs/token
// @scope.org#write modify orgs
// @scope.org#read read orgs

// @Security ClientCredentials

func main() {
	utils.InitEnv()

	dbErr := utils.OpenDb()
	if dbErr != nil {
		panic(dbErr)
	}

	r := gin.Default()

	corsConfig := cors.DefaultConfig()
	corsConfig.AllowAllOrigins = true
	r.Use(cors.New(corsConfig))
	router.LoadRoutes(r)

	r.Run()
}
