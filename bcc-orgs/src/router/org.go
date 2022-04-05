package router

import (
	"bcc-orgs/src/controller"

	"github.com/gin-gonic/gin"
)

func LoadOrgRoutes(r *gin.Engine) {
	orgSvc := r.Group("/org-test")
	orgController := controller.OrgController{}
	orgSvc.GET("/:id", orgController.Get)
	orgSvc.GET("/", orgController.Find)
	orgSvc.POST("/", orgController.Create)
	orgSvc.PUT("/:id", orgController.Update)
}
