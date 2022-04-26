package main

import (
	"bcc-orgs/src/router"

	"github.com/gin-gonic/gin"
	"github.com/joho/godotenv"
)

func main() {
	godotenv.Load()

	r := gin.Default()
	router.LoadRoutes(r)
	r.Run()
}
