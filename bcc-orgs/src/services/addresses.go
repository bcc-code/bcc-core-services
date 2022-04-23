package services

import (
	"bcc-orgs/src/models"
)

func CreateAddress(address models.Address) (*int, error) {
	var lastInsertId = 0
	err := Db.QueryRow(`
		INSERT INTO address (
			street_1,
			street_2,
			city,
			region, 
			country_iso_2_code,
			postal_code,
			country_name,
			country_name_native
		) VALUES ($1, $2, $3, $4, $5, $6, $7, $8) RETURNING address_id`,
		address.Street1,
		address.Street2,
		address.City,
		address.Region,
		address.CountryIso2Code,
		address.PostalCode,
		address.CountryName,
		address.CountryNameNative,
	).Scan(&lastInsertId)
	if err != nil {
		return nil, err
	}

	return &lastInsertId, nil
}

func CreateOrgAddresses(org models.Org) (*int, *int, *int, error) {
	var visitingAddressID *int = nil
	var postalAddressID *int = nil
	var billingAddressID *int = nil

	if addressEntered(org.VisitingAddress) {
		visitingAddressID, err = CreateAddress(org.VisitingAddress)
		if err != nil {
			panic(err)
		}
	}
	if addressEntered(org.PostalAddress) {
		postalAddressID, err = CreateAddress(org.PostalAddress)
		if err != nil {
			panic(err)
		}
	}
	if addressEntered(org.BillingAddress) {
		billingAddressID, err = CreateAddress(org.BillingAddress)
		if err != nil {
			panic(err)
		}
	}

	return visitingAddressID, postalAddressID, billingAddressID, nil
}

func addressEntered(address models.Address) bool {
	return (models.Address{}) != address
}
