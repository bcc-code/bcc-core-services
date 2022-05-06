package models

type Address struct {
	Street1           *string `json:"street1" extensions:"x-order=1"`
	Street2           *string `json:"street2" extensions:"x-order=2"`
	City              *string `json:"city" extensions:"x-order=3"`
	Region            *string `json:"region" extensions:"x-order=4"`
	CountryIso2Code   *string `json:"countryIso2Code" extensions:"x-order=5"`
	PostalCode        *string `json:"postalCode" extensions:"x-order=6"`
	CountryName       *string `json:"countryName" extensions:"x-order=7"`
	CountryNameNative *string `json:"countryNameNative" extensions:"x-order=8"`
} //@name Address
