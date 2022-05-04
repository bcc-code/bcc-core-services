package models

type ApiAddress struct {
	Street1           string `json:"street1"`
	Street2           string `json:"street2"`
	City              string `json:"city"`
	Region            string `json:"region"`
	CountryIso2Code   string `json:"countryIso2Code"`
	PostalCode        string `json:"postalCode"`
	CountryName       string `json:"countryName"`
	CountryNameNative string `json:"countryNameNative"`
}

type ApiAddressUpdate struct {
	Street1           *string `json:"street1"`
	Street2           *string `json:"street2"`
	City              *string `json:"city"`
	Region            *string `json:"region"`
	CountryIso2Code   *string `json:"countryIso2Code"`
	PostalCode        *string `json:"postalCode"`
	CountryName       *string `json:"countryName"`
	CountryNameNative *string `json:"countryNameNative"`
}

type DbAddress struct {
	AddressID         int    `gorm:"primaryKey"`
	Street1           string `gorm:"column:street_1"`
	Street2           string `gorm:"column:street_2"`
	City              string
	Region            string
	CountryIso2Code   string `gorm:"column:country_iso_2_code"`
	PostalCode        string
	CountryName       string
	CountryNameNative string
}

type DbAddressUpdate struct {
	Street1           *string `gorm:"column:street_1"`
	Street2           *string `gorm:"column:street_2"`
	City              *string
	Region            *string
	CountryIso2Code   *string `gorm:"column:country_iso_2_code"`
	PostalCode        *string
	CountryName       *string
	CountryNameNative *string
}

func (DbAddress) TableName() string {
	return "address"
}
