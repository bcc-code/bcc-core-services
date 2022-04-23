package services

import (
	"bcc-orgs/src/models"
)

func CreateAddress(address models.Address) (*int, error) {
	db := OpenDb()
	defer db.Close()

	var lastInsertId = 0
	err := db.QueryRow(`
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
