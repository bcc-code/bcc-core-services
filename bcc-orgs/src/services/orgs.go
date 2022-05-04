package services

// import (
// 	"bcc-orgs/src/models"

// 	"bcc-orgs/src/utils"

// 	"github.com/imdario/mergo"
// )

// func FindOrgs() ([]models.Org, error) {
// 	var orgs []models.Org

// 	err := utils.Db.Select(&orgs, `
// 		SELECT
// 			o.org_id AS org_id,
// 			o.name AS name,
// 			o.legal_name AS legal_name,
// 			o.type AS type,
// 			va.address_id AS "va.address_id",
// 			va.street_1 AS "va.street_1",
// 			va.street_2 AS "va.street_2",
// 			va.city AS "va.city",
// 			va.region AS "va.region",
// 			va.country_iso_2_code AS "va.country_iso_2_code",
// 			va.postal_code AS "va.postal_code",
// 			va.country_name AS "va.country_name",
// 			va.country_name_native AS "va.country_name_native",
// 			pa.address_id AS "pa.address_id",
// 			pa.street_1 AS "pa.street_1",
// 			pa.street_2 AS "pa.street_2",
// 			pa.city AS "pa.city",
// 			pa.region AS "pa.region",
// 			pa.country_iso_2_code AS "pa.country_iso_2_code",
// 			pa.postal_code AS "pa.postal_code",
// 			pa.country_name AS "pa.country_name",
// 			pa.country_name_native AS "pa.country_name_native",
// 			ba.address_id AS "ba.address_id",
// 			ba.address_id AS "ba.address_id",
// 			ba.street_1 AS "ba.street_1",
// 			ba.street_2 AS "ba.street_2",
// 			ba.city AS "ba.city",
// 			ba.region AS "ba.region",
// 			ba.country_iso_2_code AS "ba.country_iso_2_code",
// 			ba.postal_code AS "ba.postal_code",
// 			ba.country_name AS "ba.country_name",
// 			ba.country_name_native AS "ba.country_name_native"
// 		FROM org AS o
// 			LEFT JOIN address AS va ON va.address_id = o.fk_visiting_address_id
// 			LEFT JOIN address AS pa ON pa.address_id = o.fk_postal_address_id
// 			LEFT JOIN address AS ba ON ba.address_id = o.fk_billing_address_id
// 		`)

// 	return orgs, err
// }

// func GetOrg(orgID int) (models.Org, error) {
// 	var org models.Org

// 	err := utils.Db.Get(&org, `
// 		SELECT
// 			o.org_id AS org_id,
// 			o.name AS name,
// 			o.legal_name AS legal_name,
// 			o.type AS type,
// 			va.address_id AS "va.address_id",
// 			va.street_1 AS "va.street_1",
// 			va.street_2 AS "va.street_2",
// 			va.city AS "va.city",
// 			va.region AS "va.region",
// 			va.country_iso_2_code AS "va.country_iso_2_code",
// 			va.postal_code AS "va.postal_code",
// 			va.country_name AS "va.country_name",
// 			va.country_name_native AS "va.country_name_native",
// 			pa.address_id AS "pa.address_id",
// 			pa.street_1 AS "pa.street_1",
// 			pa.street_2 AS "pa.street_2",
// 			pa.city AS "pa.city",
// 			pa.region AS "pa.region",
// 			pa.country_iso_2_code AS "pa.country_iso_2_code",
// 			pa.postal_code AS "pa.postal_code",
// 			pa.country_name AS "pa.country_name",
// 			pa.country_name_native AS "pa.country_name_native",
// 			ba.address_id AS "ba.address_id",
// 			ba.address_id AS "ba.address_id",
// 			ba.street_1 AS "ba.street_1",
// 			ba.street_2 AS "ba.street_2",
// 			ba.city AS "ba.city",
// 			ba.region AS "ba.region",
// 			ba.country_iso_2_code AS "ba.country_iso_2_code",
// 			ba.postal_code AS "ba.postal_code",
// 			ba.country_name AS "ba.country_name",
// 			ba.country_name_native AS "ba.country_name_native"
// 		FROM org AS o
// 			LEFT JOIN address AS va ON va.address_id = o.fk_visiting_address_id
// 			LEFT JOIN address AS pa ON pa.address_id = o.fk_postal_address_id
// 			LEFT JOIN address AS ba ON ba.address_id = o.fk_billing_address_id
// 		WHERE o.org_id = $1
// 	`, orgID)

// 	return org, err
// }

// func CreateOrg(org models.Org) (models.Org, error) {
// 	var result models.Org

// 	visitingAddress, postalAddress, billingAddress, err := CreateOrUpdateOrgAddresses(org, models.Org{})
// 	if err != nil {
// 		return result, err
// 	}

// 	var visitingAddressID *int
// 	var postalAddressID *int
// 	var billingAddressID *int

// 	if visitingAddress != nil {
// 		result.VisitingAddress = *visitingAddress
// 		visitingAddressID = visitingAddress.AddressID
// 	}
// 	if postalAddress != nil {
// 		result.PostalAddress = *postalAddress
// 		postalAddressID = postalAddress.AddressID
// 	}
// 	if billingAddress != nil {
// 		result.BillingAddress = *billingAddress
// 		billingAddressID = billingAddress.AddressID
// 	}

// 	err = utils.Db.QueryRow(`
// 		INSERT INTO org (
// 			name,
// 			legal_name,
// 			type,
// 			fk_visiting_address_id,
// 			fk_postal_address_id,
// 			fk_billing_address_id
// 		) VALUES ($1, $2, $3, $4, $5, $6)
// 		RETURNING org_id, name, legal_name, type`, org.Name, org.LegalName, org.Type, visitingAddressID, postalAddressID, billingAddressID).Scan(&result.OrgID, &result.Name, &result.LegalName, &result.Type)

// 	return result, err
// }

// func UpdateOrg(orgID int, org models.Org) (models.Org, error) {
// 	var result models.Org

// 	currentOrg, err := GetOrg(orgID)
// 	if err != nil {
// 		return result, err
// 	}

// 	if err := mergo.Merge(&org, currentOrg); err != nil {
// 		return result, err
// 	}

// 	visitingAddress, postalAddress, billingAddress, err := CreateOrUpdateOrgAddresses(org, currentOrg)
// 	if err != nil {
// 		return result, err
// 	}

// 	var visitingAddressID *int
// 	var postalAddressID *int
// 	var billingAddressID *int

// 	if visitingAddress != nil {
// 		result.VisitingAddress = *visitingAddress
// 		visitingAddressID = visitingAddress.AddressID
// 	}
// 	if postalAddress != nil {
// 		result.PostalAddress = *postalAddress
// 		postalAddressID = postalAddress.AddressID
// 	}
// 	if billingAddress != nil {
// 		result.BillingAddress = *billingAddress
// 		billingAddressID = billingAddress.AddressID
// 	}

// 	err = utils.Db.QueryRow(`
// 		UPDATE org
// 			SET name = $2, legal_name = $3, type = $4, fk_visiting_address_id = $5, fk_postal_address_id = $6, fk_billing_address_id = $7
// 		WHERE org_id = $1
// 		RETURNING org_id, name, legal_name, type`, orgID, org.Name, org.LegalName, org.Type, visitingAddressID, postalAddressID, billingAddressID).Scan(&result.OrgID, &result.Name, &result.LegalName, &result.Type)

// 	return result, err
// }
