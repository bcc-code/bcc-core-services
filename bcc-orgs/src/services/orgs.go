package services

import (
	"bcc-orgs/src/models"
)

func Find() []models.Org {
	db := OpenDb()
	defer db.Close()

	var orgs []models.Org

	err := db.Select(&orgs, "SELECT o.org_id AS org_id, o.name AS name, o.legal_name AS legal_name, o.type AS type, va.address_id AS visiting_address_id, va.street_1 AS visiting_address_street_1, va.street_2 AS visiting_address_street_2, va.city AS visiting_address_city,    va.region AS visiting_address_region,    va.country_iso_2_code AS visiting_address_country_iso_2_code,    va.postal_code AS visiting_address_postal_code,    va.country_name AS visiting_address_country_name,    va.country_name_native AS visiting_address_country_name_native,    pa.address_id AS postal_address_id,    pa.street_1 AS postal_address_street_1,    pa.street_2 AS postal_address_street_2,    pa.city AS postal_address_city,    pa.region AS postal_address_region,    pa.country_iso_2_code AS postal_address_country_iso_2_code,    pa.postal_code AS postal_address_postal_code,    pa.country_name AS postal_address_country_name,    pa.country_name_native AS postal_address_country_name_native,    ba.address_id AS billing_address_id,    ba.street_1 AS billing_address_street_1,    ba.street_2 AS billing_address_street_2,    ba.city AS billing_address_city,    ba.region AS billing_address_region,    ba.country_iso_2_code AS billing_address_country_iso_2_code,    ba.postal_code AS billing_address_postal_code,    ba.country_name AS billing_address_country_name,    ba.country_name_native AS billing_address_country_name_native FROM org AS o   LEFT JOIN address AS va ON va.address_id = o.fk_visiting_address_id   LEFT JOIN address AS pa ON pa.address_id = o.fk_postal_address_id   LEFT JOIN address AS ba ON ba.address_id = o.fk_billing_address_id")
	if err != nil {
		panic(err)
	}

	return orgs
}
