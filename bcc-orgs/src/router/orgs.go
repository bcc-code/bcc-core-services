package router

import (
	"bcc-orgs/src/auth"
	"bcc-orgs/src/controllers"

	"github.com/gin-gonic/gin"
)

func LoadOrgsRoutes(r *gin.Engine) {
	orgsSvc := r.Group("/orgs")
	orgsController := controllers.OrgsController{}
	orgsSvc.GET("/:orgID", auth.JWTCheckWithScopes([]string{"read:org"}), orgsController.Get)
	orgsSvc.GET("/", auth.JWTCheckWithScopes([]string{"read:org"}), orgsController.Find)
	orgsSvc.POST("/", auth.JWTCheckWithScopes([]string{"write:org"}), orgsController.Create)
	orgsSvc.PUT("/:orgID", auth.JWTCheckWithScopes([]string{"write:org"}), orgsController.Update)
}
