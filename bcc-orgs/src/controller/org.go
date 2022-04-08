package controller

import (
	"net/http"

	"github.com/gin-gonic/gin"
)

type OrgController struct{}

type Org struct {
	Id   int    `json:"id"`
	Name string `json:"name"`
}

func (ctrl OrgController) Get(c *gin.Context) {
	c.JSON(http.StatusOK, Org{})
}
func (ctrl OrgController) Find(c *gin.Context) {
	c.JSON(http.StatusOK, []Org{{}, {}})
}
func (ctrl OrgController) Create(c *gin.Context) {
	c.JSON(http.StatusOK, Org{})
}
func (ctrl OrgController) Update(c *gin.Context) {
	c.JSON(http.StatusOK, Org{})
}
