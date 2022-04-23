package models

type VisitingAddressEntity struct {
	AddressID         *int    `json:"addressID" db:"visiting_address_id"`
	Street1           *string `json:"street1" db:"visiting_address_street_1"`
	Street2           *string `json:"street2" db:"visiting_address_street_2"`
	City              *string `json:"city" db:"visiting_address_city"`
	Region            *string `json:"region" db:"visiting_address_region"`
	CountryIso2Code   *string `json:"countryIso2Code" db:"visiting_address_country_iso_2_code"`
	PostalCode        *string `json:"postalCode" db:"visiting_address_postal_code"`
	CountryName       *string `json:"countryName" db:"visiting_address_country_name"`
	CountryNameNative *string `json:"countryNameNative" db:"visiting_address_country_name_native"`
}

type PostalAddressEntity struct {
	AddressID         *int    `json:"addressID" db:"postal_address_id"`
	Street1           *string `json:"street1" db:"postal_address_street_1"`
	Street2           *string `json:"street2" db:"postal_address_street_2"`
	City              *string `json:"city" db:"postal_address_city"`
	Region            *string `json:"region" db:"postal_address_region"`
	CountryIso2Code   *string `json:"countryIso2Code" db:"postal_address_country_iso_2_code"`
	PostalCode        *string `json:"postalCode" db:"postal_address_postal_code"`
	CountryName       *string `json:"countryName" db:"postal_address_country_name"`
	CountryNameNative *string `json:"countryNameNative" db:"postal_address_country_name_native"`
}

type BillingAddressEntity struct {
	AddressID         *int    `json:"addressID" db:"billing_address_id"`
	Street1           *string `json:"street1" db:"billing_address_street_1"`
	Street2           *string `json:"street2" db:"billing_address_street_2"`
	City              *string `json:"city" db:"billing_address_city"`
	Region            *string `json:"region" db:"billing_address_region"`
	CountryIso2Code   *string `json:"countryIso2Code" db:"billing_address_country_iso_2_code"`
	PostalCode        *string `json:"postalCode" db:"billing_address_postal_code"`
	CountryName       *string `json:"countryName" db:"billing_address_country_name"`
	CountryNameNative *string `json:"countryNameNative" db:"billing_address_country_name_native"`
}

type AddressEntity struct {
	Address_id          int
	Street_1            string
	Street_2            string
	City                string
	Region              string
	Country_iso_2_code  string
	Postal_code         string
	Country_name        string
	Country_name_native string
}

type OrgAssociationEntity struct {
	Association_id int
	Fk_parent_id   int
	Fk_child_id    int
}
