package main

import (
	"bcc-orgs/src/router"

	"bcc-orgs/src/services"

	"github.com/gin-gonic/gin"
	"github.com/joho/godotenv"
)

func main() {
	godotenv.Load()

	services.OpenDb()

	r := gin.Default()
	router.LoadRoutes(r)

	r.Run()
}
