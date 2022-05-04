package services

// import (
// 	"bcc-orgs/src/models"
// 	"bcc-orgs/src/utils"
// 	"fmt"
// )

// func CreateAddress(address models.Address) (*models.Address, error) {
// 	var result models.Address
// 	err := utils.Db.QueryRow(`
// 		INSERT INTO address (
// 			street_1,
// 			street_2,
// 			city,
// 			region,
// 			country_iso_2_code,
// 			postal_code,
// 			country_name,
// 			country_name_native
// 		) VALUES ($1, $2, $3, $4, $5, $6, $7, $8)
// 		RETURNING *`,
// 		address.Street1,
// 		address.Street2,
// 		address.City,
// 		address.Region,
// 		address.CountryIso2Code,
// 		address.PostalCode,
// 		address.CountryName,
// 		address.CountryNameNative,
// 	).Scan(
// 		&result.AddressID,
// 		&result.Street1,
// 		&result.Street2,
// 		&result.City,
// 		&result.Region,
// 		&result.CountryIso2Code,
// 		&result.PostalCode,
// 		&result.CountryName,
// 		&result.CountryNameNative)

// 	return &result, err
// }

// func UpdateAddress(addressID int, address models.Address) (*models.Address, error) {
// 	var result models.Address
// 	err := utils.Db.QueryRow(`
// 		UPDATE address
// 			SET street_1 = $2, street_2 = $3, city = $4, region = $5, country_iso_2_code = $6, postal_code = $7, country_name = $8, country_name_native = $9
// 		WHERE address_id = $1
// 		RETURNING *`,
// 		addressID,
// 		address.Street1,
// 		address.Street2,
// 		address.City,
// 		address.Region,
// 		address.CountryIso2Code,
// 		address.PostalCode,
// 		address.CountryName,
// 		address.CountryNameNative,
// 	).Scan(
// 		&result.AddressID,
// 		&result.Street1,
// 		&result.Street2,
// 		&result.City,
// 		&result.Region,
// 		&result.CountryIso2Code,
// 		&result.PostalCode,
// 		&result.CountryName,
// 		&result.CountryNameNative)

// 	return &result, err
// }

// func CreateOrUpdateOrgAddresses(changedOrg models.Org, current models.Org) (*models.Address, *models.Address, *models.Address, error) {
// 	visitingAddress, err := checkAndSaveAddress(changedOrg.VisitingAddress, current.VisitingAddress)
// 	if err != nil {
// 		return nil, nil, nil, err
// 	}
// 	postalAddress, err := checkAndSaveAddress(changedOrg.PostalAddress, current.PostalAddress)
// 	if err != nil {
// 		return nil, nil, nil, err
// 	}
// 	billingAddress, err := checkAndSaveAddress(changedOrg.BillingAddress, current.BillingAddress)
// 	if err != nil {
// 		return nil, nil, nil, err
// 	}

// 	return visitingAddress, postalAddress, billingAddress, nil
// }

// func checkAndSaveAddress(changedAddress models.Address, currentAddress models.Address) (*models.Address, error) {
// 	var err error
// 	var address *models.Address = &currentAddress

// 	if addressEntered(changedAddress) == false {
// 		return address, nil
// 	}

// 	existingAddressID := currentAddress.AddressID

// 	if existingAddressID == nil {
// 		address, err = CreateAddress(changedAddress)
// 		fmt.Printf("%+v\n", *address)
// 	} else {
// 		address, err = UpdateAddress(*existingAddressID, changedAddress)
// 	}
// 	return address, err
// }

// func addressEntered(address models.Address) bool {
// 	return (models.Address{}) != address
// }
