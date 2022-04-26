package services

import (
	"bcc-orgs/src/models"
	"bcc-orgs/src/utils"
)

func CreateAddress(address models.Address) (*models.Address, error) {
	var result models.Address
	err := utils.Db.QueryRow(`
		INSERT INTO address (
			street_1,
			street_2,
			city,
			region, 
			country_iso_2_code,
			postal_code,
			country_name,
			country_name_native
		) VALUES ($1, $2, $3, $4, $5, $6, $7, $8)
		RETURNING *`,
		address.Street1,
		address.Street2,
		address.City,
		address.Region,
		address.CountryIso2Code,
		address.PostalCode,
		address.CountryName,
		address.CountryNameNative,
	).Scan(
		&result.AddressID,
		&result.Street1,
		&result.Street2,
		&result.City,
		&result.Region,
		&result.CountryIso2Code,
		&result.PostalCode,
		&result.CountryName,
		&result.CountryNameNative)

	return &result, err
}

func UpdateAddress(addressID int, address models.Address) (*models.Address, error) {
	var result models.Address
	err := utils.Db.QueryRow(`
		UPDATE address
			SET street_1 = $2, street_2 = $3, city = $4, region = $5, country_iso_2_code = $6, postal_code = $7, country_name = $8, country_name_native = $9
		WHERE address_id = $1
		RETURNING *`,
		addressID,
		address.Street1,
		address.Street2,
		address.City,
		address.Region,
		address.CountryIso2Code,
		address.PostalCode,
		address.CountryName,
		address.CountryNameNative,
	).Scan(
		&result.AddressID,
		&result.Street1,
		&result.Street2,
		&result.City,
		&result.Region,
		&result.CountryIso2Code,
		&result.PostalCode,
		&result.CountryName,
		&result.CountryNameNative)
	if err != nil {
		return &result, err
	}

	return &result, nil
}

func CreateOrUpdateOrgAddresses(current models.Org, changedOrg models.Org) (*models.Address, *models.Address, *models.Address, error) {
	visitingAddress := checkAndSaveAddress(changedOrg.VisitingAddress, current.VisitingAddress)
	postalAddress := checkAndSaveAddress(changedOrg.PostalAddress, current.PostalAddress)
	billingAddress := checkAndSaveAddress(changedOrg.BillingAddress, current.BillingAddress)

	return visitingAddress, postalAddress, billingAddress, nil
}

func checkAndSaveAddress(changedAddress models.Address, currentAddress models.Address) *models.Address {
	var address *models.Address = &currentAddress

	if addressEntered(changedAddress) == false {
		return address
	}

	existingAddressID := currentAddress.AddressID
	var err error

	if existingAddressID == nil {
		address, err = CreateAddress(changedAddress)
	} else {
		address, err = UpdateAddress(*existingAddressID, changedAddress)
	}
	if err != nil {
		panic(err)
	}
	return address
}

func addressEntered(address models.Address) bool {
	return (models.Address{}) != address
}
