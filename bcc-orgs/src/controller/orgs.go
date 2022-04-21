package controller

import (
	"net/http"

	"bcc-orgs/src/models"
	"bcc-orgs/src/services"

	"github.com/gin-gonic/gin"
)

type OrgsController struct{}

func (ctrl OrgsController) Get(c *gin.Context) {
	c.JSON(http.StatusOK, models.Org{})
}
func (ctrl OrgsController) Find(c *gin.Context) {
	orgs := services.Query()
	c.JSON(http.StatusOK, orgs)
}
func (ctrl OrgsController) Create(c *gin.Context) {
	c.JSON(http.StatusOK, models.Org{})
}
func (ctrl OrgsController) Update(c *gin.Context) {
	c.JSON(http.StatusOK, models.Org{})
}
