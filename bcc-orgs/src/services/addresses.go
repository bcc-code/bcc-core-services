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

func CreateOrgAddresses(org models.Org) (*models.Address, *models.Address, *models.Address, error) {
	var visitingAddress *models.Address
	var postalAddress *models.Address
	var billingAddress *models.Address

	if addressEntered(org.VisitingAddress) {
		var err error
		visitingAddress, err = CreateAddress(org.VisitingAddress)
		if err != nil {
			panic(err)
		}
	}
	if addressEntered(org.PostalAddress) {
		var err error
		postalAddress, err = CreateAddress(org.PostalAddress)
		if err != nil {
			panic(err)
		}
	}
	if addressEntered(org.BillingAddress) {
		var err error
		billingAddress, err = CreateAddress(org.BillingAddress)
		if err != nil {
			panic(err)
		}
	}

	return visitingAddress, postalAddress, billingAddress, nil
}

func UpdateOrgAddresses(current models.Org, changedOrg models.Org) (*models.Address, *models.Address, *models.Address, error) {
	var visitingAddress *models.Address
	var postalAddress *models.Address
	var billingAddress *models.Address

	if addressEntered(changedOrg.VisitingAddress) {
		var err error
		visitingAddressID := current.VisitingAddress.AddressID
		if visitingAddressID == nil {
			visitingAddress, err = CreateAddress(changedOrg.VisitingAddress)
		} else {
			visitingAddress, err = UpdateAddress(*visitingAddressID, changedOrg.VisitingAddress)
		}
		if err != nil {
			panic(err)
		}
	}

	if addressEntered(changedOrg.PostalAddress) {
		var err error
		postalAddressID := current.PostalAddress.AddressID
		if postalAddressID == nil {
			postalAddress, err = CreateAddress(changedOrg.PostalAddress)
		} else {
			postalAddress, err = UpdateAddress(*postalAddressID, changedOrg.PostalAddress)
		}
		if err != nil {
			panic(err)
		}
	}

	if addressEntered(changedOrg.BillingAddress) {
		var err error
		billingAddressID := current.BillingAddress.AddressID

		if billingAddressID == nil {
			billingAddress, err = CreateAddress(changedOrg.BillingAddress)
		} else {
			billingAddress, err = UpdateAddress(*billingAddressID, changedOrg.BillingAddress)
		}
		if err != nil {
			panic(err)
		}
	}

	return visitingAddress, postalAddress, billingAddress, nil
}

func addressEntered(address models.Address) bool {
	return (models.Address{}) != address
}
