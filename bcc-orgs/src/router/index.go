package router

import "github.com/gin-gonic/gin"

func LoadRoutes(r *gin.Engine) {
	LoadOrgRoutes(r)
}
