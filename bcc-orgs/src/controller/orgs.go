package controller

import (
	"net/http"
	"strconv"

	"bcc-orgs/src/models"
	"bcc-orgs/src/services"

	"github.com/gin-gonic/gin"
)

type OrgsController struct{}

func (ctrl OrgsController) Get(c *gin.Context) {
	idString := c.Param("id")
	orgID, _ := strconv.Atoi(idString)

	org, err := services.GetOrg(orgID)

	if err != nil {
		c.JSON(http.StatusNotFound, gin.H{"error": true, "message": err.Error()})
	} else {
		c.JSON(http.StatusOK, org)
	}
}
func (ctrl OrgsController) Find(c *gin.Context) {
	orgs := services.FindOrgs()
	c.JSON(http.StatusOK, orgs)
}
func (ctrl OrgsController) Create(c *gin.Context) {
	c.JSON(http.StatusOK, models.Org{})
}
func (ctrl OrgsController) Update(c *gin.Context) {
	c.JSON(http.StatusOK, models.Org{})
}
