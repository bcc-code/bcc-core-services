package models

type Address struct {
	AddressID         *int    `json:"-" db:"address_id"`
	Street1           *string `json:"street1" db:"street_1" extensions:"x-order=1"`
	Street2           *string `json:"street2" db:"street_2" extensions:"x-order=2"`
	City              *string `json:"city" db:"city" extensions:"x-order=3"`
	Region            *string `json:"region" db:"region" extensions:"x-order=4"`
	CountryIso2Code   *string `json:"countryIso2Code" db:"country_iso_2_code" extensions:"x-order=5"`
	PostalCode        *string `json:"postalCode" db:"postal_code" extensions:"x-order=6"`
	CountryName       *string `json:"countryName" db:"country_name" extensions:"x-order=7"`
	CountryNameNative *string `json:"countryNameNative" db:"country_name_native" extensions:"x-order=8"`
} //@name Address
