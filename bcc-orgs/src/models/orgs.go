package models

type Address struct {
	AddressID         int    `json:"addressID"`
	Street1           string `json:"street1"`
	Street2           string `json:"street2"`
	City              string `json:"city"`
	Region            string `json:"region"`
	CountryIso2Code   string `json:"countryIso2Code"`
	PostalCode        string `json:"postalCode"`
	CountryName       string `json:"countryName"`
	CountryNameNative string `json:"countryNameNative"`
}
type Org struct {
	OrgID           int     `json:"orgID"`
	Name            string  `json:"name"`
	LegalName       string  `json:"legalName"`
	Type            string  `json:"type"`
	VisitingAddress Address `json:"visitingAddress"`
	PostalAddress   Address `json:"postalAddress"`
	BillingAddress  Address `json:"billingAddress"`
}
