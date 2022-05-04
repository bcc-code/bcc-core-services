package router

import (
	"github.com/gin-gonic/gin"
)

func LoadDocs(r *gin.Engine) {
	r.StaticFile("/swagger.yaml", "./swagger.yaml")
}
