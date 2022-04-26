package router

import (
	"bcc-orgs/src/auth0"
	"bcc-orgs/src/controller"

	"github.com/gin-gonic/gin"
)

func LoadOrgsRoutes(r *gin.Engine) {
	orgsSvc := r.Group("/orgs")
	orgsController := controller.OrgsController{}
	orgsSvc.GET("/:id", auth0.JWTCheckWithScopes([]string{"read:org"}), orgsController.Get)
	orgsSvc.GET("/", auth0.JWTCheckWithScopes([]string{"read:org"}), orgsController.Find)
	orgsSvc.POST("/", auth0.JWTCheckWithScopes([]string{"write:org"}), orgsController.Create)
	orgsSvc.PUT("/:id", auth0.JWTCheckWithScopes([]string{"write:org"}), orgsController.Update)
}
