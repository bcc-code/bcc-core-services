package services

import (
	"bcc-orgs/src/models"
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestCreateOrgAddresses(t *testing.T) {

	visitingAddressStreet := "Street 1"
	postalAddressCity := "City"
	billingAddressRegion := "Area"

	orgWithAddresses := models.Org{
		VisitingAddress: models.Address{
			Street1: &visitingAddressStreet,
		},
		PostalAddress: models.Address{
			City: &postalAddressCity,
		},
		BillingAddress: models.Address{
			Region: &billingAddressRegion,
		},
	}

	visitingAddress, postalAddress, billingAddress, err := CreateOrUpdateOrgAddresses(orgWithAddresses, models.Org{})
	if err != nil {
		panic(err)
	}

	assert.NotEmpty(t, *visitingAddress.AddressID, "Visiting Address ID was not correct")
	assert.NotEmpty(t, *postalAddress.AddressID, "Postal Address ID was not correct")
	assert.NotEmpty(t, *billingAddress.AddressID, "Billing Address ID was not correct")

	assert.Equal(t, *visitingAddress.Street1, visitingAddressStreet, "Visiting Address Street 1 was not correct")
	assert.Equal(t, *postalAddress.City, postalAddressCity, "Postal Address City was not correct")
	assert.Equal(t, *billingAddress.Region, billingAddressRegion, "Billing Address Region was not correct")
}
