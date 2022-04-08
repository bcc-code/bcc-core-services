package controller

import (
	"net/http"

	"bcc-orgs/src/models"
	"bcc-orgs/src/services"

	"github.com/gin-gonic/gin"
)

type OrgController struct{}

func (ctrl OrgController) Get(c *gin.Context) {
	c.JSON(http.StatusOK, models.Org{})
}
func (ctrl OrgController) Find(c *gin.Context) {
	orgs := services.Query()
	c.JSON(http.StatusOK, orgs)
}
func (ctrl OrgController) Create(c *gin.Context) {
	c.JSON(http.StatusOK, models.Org{})
}
func (ctrl OrgController) Update(c *gin.Context) {
	c.JSON(http.StatusOK, models.Org{})
}
