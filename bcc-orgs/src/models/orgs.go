package models

import (
	"bcc-orgs/src/sqlc"
	"fmt"
)

type Org struct {
	OrgID           int32   `json:"orgID" extensions:"x-order=1"`
	Name            string  `json:"name" extensions:"x-order=2"`
	LegalName       *string `json:"legalName" extensions:"x-order=3"`
	Type            string  `json:"type" extensions:"x-order=4"`
	VisitingAddress Address `json:"visitingAddress" extensions:"x-order=5"`
	PostalAddress   Address `json:"postalAddress" extensions:"x-order=6"`
	BillingAddress  Address `json:"billingAddress" extensions:"x-order=7"`
} //@name Org

func DbOrgToOrg(dbOrg sqlc.GetOrgRow) Org {
	return Org{
		OrgID:           dbOrg.OrgID,
		Name:            dbOrg.Name,
		LegalName:       &dbOrg.LegalName.String,
		Type:            dbOrg.Type,
		VisitingAddress: DbOrgExtractAddress(dbOrg, "Va"),
		PostalAddress:   DbOrgExtractAddress(dbOrg, "Pa"),
		BillingAddress:  DbOrgExtractAddress(dbOrg, "Ba"),
	}
}

func DbOrgExtractAddress(dbOrg sqlc.GetOrgRow, addressType string) Address {
	fmt.Printf("%+v\n", dbOrg)
	return Address{}
}
