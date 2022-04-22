package models

type OrgEntity struct {
	Org_id                               int
	Name                                 string
	Legal_name                           string
	Type                                 string
	Visiting_address_address_id          int
	Visiting_address_street_1            string
	Visiting_address_street_2            string
	Visiting_address_city                string
	Visiting_address_region              string
	Visiting_address_country_iso_2_code  string
	Visiting_address_postal_code         string
	Visiting_address_country_name        string
	Visiting_address_country_name_native string
	Postal_address_address_id            int
	Postal_address_street_1              string
	Postal_address_street_2              string
	Postal_address_city                  string
	Postal_address_region                string
	Postal_address_country_iso_2_code    string
	Postal_address_postal_code           string
	Postal_address_country_name          string
	Postal_address_country_name_native   string
	Billing_address_address_id           int
	Billing_address_street_1             string
	Billing_address_street_2             string
	Billing_address_city                 string
	Billing_address_region               string
	Billing_address_country_iso_2_code   string
	Billing_address_postal_code          string
	Billing_address_country_name         string
	Billing_address_country_name_native  string
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
