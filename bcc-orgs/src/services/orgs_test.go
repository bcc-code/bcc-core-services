package services

import (
	"bcc-orgs/src/models"
	"bcc-orgs/src/utils"
	"fmt"
	"os"
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestMain(m *testing.M) {

	dbErr := utils.OpenDb()
	if dbErr != nil {
		panic(dbErr)
	} else {
		fmt.Println("DB connected")
	}
	exitVal := m.Run()
	os.Exit(exitVal)
}

func TestCreateOrg(t *testing.T) {
	legalName := "BCC Svartskog"
	org := models.Org{
		Name:      "Svartskog",
		LegalName: &legalName,
		Type:      "Church",
	}

	createdOrgID, err := CreateOrg(org)
	if err != nil {
		panic(err)
	}

	createdOrg, err := GetOrg(createdOrgID)
	if err != nil {
		panic(err)
	}

	assert.Equal(t, createdOrg.Name, org.Name, "Org Name was not correct")
	assert.Equal(t, createdOrg.LegalName, org.LegalName, "Org LegalName was not correct")
	assert.Equal(t, createdOrg.Type, org.Type, "Org Type was not correct")
	assert.Equal(t, createdOrg.VisitingAddress.AddressID, org.VisitingAddress.AddressID, "Org VisitingAddress was not correct")
	assert.Equal(t, createdOrg.BillingAddress.AddressID, org.BillingAddress.AddressID, "Org BillingAddress was not correct")
	assert.Equal(t, createdOrg.PostalAddress.AddressID, org.PostalAddress.AddressID, "Org PostalAddress was not correct")
}

func TestFindOrgs(t *testing.T) {
	orgs, err := FindOrgs()
	if err != nil {
		panic(err)
	}

	assert.True(t, len(orgs) >= 4, "Orgs count was too low")
	for _, org := range orgs {
		assert.NotEmpty(t, org.OrgID, "OrgID was not correct")
		assert.NotEmpty(t, org.Name, "Name was not correct")
		assert.NotEmpty(t, org.Type, "Type was not correct")
	}
}
