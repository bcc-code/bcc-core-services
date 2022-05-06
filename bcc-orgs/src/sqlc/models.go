// Code generated by sqlc. DO NOT EDIT.
// versions:
//   sqlc v1.13.0

package sqlc

import (
	"database/sql"
)

type Address struct {
	AddressID         int32          `db:"address_id"`
	Street1           sql.NullString `db:"street_1"`
	Street2           sql.NullString `db:"street_2"`
	City              sql.NullString `db:"city"`
	Region            sql.NullString `db:"region"`
	CountryIso2Code   sql.NullString `db:"country_iso_2_code"`
	PostalCode        sql.NullString `db:"postal_code"`
	CountryName       sql.NullString `db:"country_name"`
	CountryNameNative sql.NullString `db:"country_name_native"`
}

type Org struct {
	OrgID               int32          `db:"org_id"`
	Name                string         `db:"name"`
	LegalName           sql.NullString `db:"legal_name"`
	Type                string         `db:"type"`
	FkVisitingAddressID sql.NullInt32  `db:"fk_visiting_address_id"`
	FkPostalAddressID   sql.NullInt32  `db:"fk_postal_address_id"`
	FkBillingAddressID  sql.NullInt32  `db:"fk_billing_address_id"`
}

type OrgAssociation struct {
	AssociationID int32 `db:"association_id"`
	FkParentID    int32 `db:"fk_parent_id"`
	FkChildID     int32 `db:"fk_child_id"`
}