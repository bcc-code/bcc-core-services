package models

type Address struct {
	AddressID         *int    `json:"-" db:"address_id"`
	Street1           *string `json:"street1" db:"street_1"`
	Street2           *string `json:"street2" db:"street_2"`
	City              *string `json:"city" db:"city"`
	Region            *string `json:"region" db:"region"`
	CountryIso2Code   *string `json:"countryIso2Code" db:"country_iso_2_code"`
	PostalCode        *string `json:"postalCode" db:"postal_code"`
	CountryName       *string `json:"countryName" db:"country_name"`
	CountryNameNative *string `json:"countryNameNative" db:"country_name_native"`
} //@name Address
