package controllers

import (
	"fmt"
	"net/http"
	"strconv"

	"bcc-orgs/src/models"
	"bcc-orgs/src/services"

	"github.com/gin-gonic/gin"
)

type OrgsController struct{}

func (ctrl OrgsController) Get(c *gin.Context) {
	idString := c.Param("id")
	orgID, convError := strconv.Atoi(idString)
	if convError != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": true, "message": "Invalid orgID was used."})
	}

	org, err := services.GormGetOrg(orgID)

	if err != nil {
		notFound := fmt.Sprintf("Organization could not be found for orgID %v", orgID)
		c.JSON(http.StatusNotFound, gin.H{"error": true, "message": notFound})
	} else {
		c.JSON(http.StatusOK, org)
	}
}

func (ctrl OrgsController) Find(c *gin.Context) {
	orgs, err := services.GormFindOrgs()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": true, "message": err.Error()})
	} else {
		c.JSON(http.StatusOK, orgs)

	}
}

func (ctrl OrgsController) Create(c *gin.Context) {
	var createData models.ApiOrgCreate

	err := c.BindJSON(&createData)
	if err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": true, "message": err.Error()})
	}

	fmt.Printf("%+v\n", createData)

	org, creationErr := services.GormCreateOrg(createData)
	if creationErr != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": true, "message": err.Error()})
	} else {
		c.JSON(http.StatusOK, org)
	}
}

func (ctrl OrgsController) Update(c *gin.Context) {
	idString := c.Param("id")
	orgID, _ := strconv.Atoi(idString)

	var org models.ApiOrgUpdate
	err := c.BindJSON(&org)

	fmt.Printf("%+v\n%+v\n%+v\n", org, orgID, err)
	updatedOrg, err := services.GormUpdateOrg(orgID, org)

	if err != nil {
		c.JSON(http.StatusNotFound, gin.H{"error": true, "message": err.Error()})
	} else {
		c.JSON(http.StatusOK, updatedOrg)
	}
}
