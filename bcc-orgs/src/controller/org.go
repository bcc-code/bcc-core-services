package controller

import (
	"fmt"
	"net/http"

	"bcc-orgs/src/services"

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
	fmt.Println("Find call")
	services.Query()
	c.JSON(http.StatusOK, []Org{{}, {}})
}
func (ctrl OrgController) Create(c *gin.Context) {
	c.JSON(http.StatusOK, Org{})
}
func (ctrl OrgController) Update(c *gin.Context) {
	c.JSON(http.StatusOK, Org{})
}
