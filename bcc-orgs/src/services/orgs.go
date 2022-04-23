package services

import (
	"bcc-orgs/src/models"
	"errors"
	"fmt"
)

func FindOrgs() []models.Org {
	var orgs []models.Org

	err := Db.Select(&orgs, `
		SELECT 
			o.org_id AS org_id, 
			o.name AS name, 
			o.legal_name AS legal_name,
			o.type AS type, 
			va.address_id AS "va.address_id",
			va.street_1 AS "va.street_1",
			va.street_2 AS "va.street_2",
			va.city AS "va.city",
			va.region AS "va.region",
			va.country_iso_2_code AS "va.country_iso_2_code",
			va.postal_code AS "va.postal_code",
			va.country_name AS "va.country_name",
			va.country_name_native AS "va.country_name_native",
			pa.address_id AS "pa.address_id",
			pa.street_1 AS "pa.street_1",
			pa.street_2 AS "pa.street_2",
			pa.city AS "pa.city",
			pa.region AS "pa.region",
			pa.country_iso_2_code AS "pa.country_iso_2_code",
			pa.postal_code AS "pa.postal_code",
			pa.country_name AS "pa.country_name",
			pa.country_name_native AS "pa.country_name_native",
			ba.address_id AS "ba.address_id",
			ba.address_id AS "ba.address_id",
			ba.street_1 AS "ba.street_1",
			ba.street_2 AS "ba.street_2",
			ba.city AS "ba.city",
			ba.region AS "ba.region",
			ba.country_iso_2_code AS "ba.country_iso_2_code",
			ba.postal_code AS "ba.postal_code",
			ba.country_name AS "ba.country_name",
			ba.country_name_native AS "ba.country_name_native"
		FROM org AS o
			LEFT JOIN address AS va ON va.address_id = o.fk_visiting_address_id
			LEFT JOIN address AS pa ON pa.address_id = o.fk_postal_address_id
			LEFT JOIN address AS ba ON ba.address_id = o.fk_billing_address_id
		`)
	if err != nil {
		panic(err)
	}

	return orgs
}

func GetOrg(orgID int) (models.Org, error) {
	var org models.Org

	err := Db.Get(&org, `
		SELECT 
			o.org_id AS org_id, 
			o.name AS name, 
			o.legal_name AS legal_name,
			o.type AS type, 
			va.address_id AS "va.address_id",
			va.street_1 AS "va.street_1",
			va.street_2 AS "va.street_2",
			va.city AS "va.city",
			va.region AS "va.region",
			va.country_iso_2_code AS "va.country_iso_2_code",
			va.postal_code AS "va.postal_code",
			va.country_name AS "va.country_name",
			va.country_name_native AS "va.country_name_native",
			pa.address_id AS "pa.address_id",
			pa.street_1 AS "pa.street_1",
			pa.street_2 AS "pa.street_2",
			pa.city AS "pa.city",
			pa.region AS "pa.region",
			pa.country_iso_2_code AS "pa.country_iso_2_code",
			pa.postal_code AS "pa.postal_code",
			pa.country_name AS "pa.country_name",
			pa.country_name_native AS "pa.country_name_native",
			ba.address_id AS "ba.address_id",
			ba.address_id AS "ba.address_id",
			ba.street_1 AS "ba.street_1",
			ba.street_2 AS "ba.street_2",
			ba.city AS "ba.city",
			ba.region AS "ba.region",
			ba.country_iso_2_code AS "ba.country_iso_2_code",
			ba.postal_code AS "ba.postal_code",
			ba.country_name AS "ba.country_name",
			ba.country_name_native AS "ba.country_name_native"
		FROM org AS o
			LEFT JOIN address AS va ON va.address_id = o.fk_visiting_address_id
			LEFT JOIN address AS pa ON pa.address_id = o.fk_postal_address_id
			LEFT JOIN address AS ba ON ba.address_id = o.fk_billing_address_id
		WHERE o.org_id = $1
	`, orgID)
	if err != nil {
		notFound := fmt.Sprintf("Organization could not be found for orgID %v", orgID)
		return org, errors.New(notFound)
	}

	return org, nil
}

func CreateOrg(org models.Org) (int, error) {
	visitingAddressID, postalAddressID, billingAddressID, addressErr := CreateOrgAddresses(org)
	if addressErr != nil {
		panic(addressErr)
	}

	lastInsertId := 0

	err := Db.QueryRow(`
		INSERT INTO org (
			name, 
			legal_name, 
			type, 
			fk_visiting_address_id,
			fk_postal_address_id,
			fk_billing_address_id
		) VALUES ($1, $2, $3, $4, $5, $6)
		RETURNING org_id`, org.Name, org.LegalName, org.Type, visitingAddressID, postalAddressID, billingAddressID).Scan(&lastInsertId)
	if err != nil {
		return 0, err
	}

	return lastInsertId, nil
}
