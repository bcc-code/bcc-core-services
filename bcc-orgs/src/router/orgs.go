package router

import (
	"bcc-orgs/src/controllers"

	"github.com/gin-gonic/gin"
)

func LoadOrgsRoutes(r *gin.Engine) {
	orgsSvc := r.Group("/orgs")
	orgsController := controllers.OrgsController{}
	orgsSvc.GET("/:id", orgsController.Get)
	orgsSvc.GET("/", orgsController.Find)
	orgsSvc.POST("/", orgsController.Create)
	orgsSvc.PUT("/:id", orgsController.Update)
}
