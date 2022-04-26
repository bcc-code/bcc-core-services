package main

import (
	"bcc-orgs/src/router"

	"github.com/gin-gonic/gin"
)

func main() {
	r := gin.Default()
	router.LoadRoutes(r)
	r.Run()
}
