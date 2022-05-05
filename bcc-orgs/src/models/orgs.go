package models

type Org struct {
	OrgID           int     `json:"orgID" db:"org_id"  extensions:"x-order=1"`
	Name            string  `json:"name" db:"name" extensions:"x-order=2"`
	LegalName       *string `json:"legalName" db:"legal_name" extensions:"x-order=3"`
	Type            string  `json:"type" db:"type" extensions:"x-order=4"`
	VisitingAddress Address `json:"visitingAddress" db:"va" extensions:"x-order=5"`
	PostalAddress   Address `json:"postalAddress" db:"pa" extensions:"x-order=6"`
	BillingAddress  Address `json:"billingAddress" db:"ba" extensions:"x-order=7"`
} //@name Org
