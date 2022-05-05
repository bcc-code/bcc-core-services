package router

import (
	"bcc-orgs/src/docs"

	"github.com/gin-gonic/gin"
)

func LoadDocs(r *gin.Engine) {
	doc := docs.SwaggerInfo.ReadDoc()
	r.GET("/swagger.json", func(c *gin.Context) {
		c.String(200, doc)
	})
}
