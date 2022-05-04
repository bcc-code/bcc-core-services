package models

type Org struct {
	OrgID           int     `json:"orgID" db:"org_id"`
	Name            string  `json:"name" db:"name"`
	LegalName       *string `json:"legalName" db:"legal_name"`
	Type            string  `json:"type" db:"type"`
	VisitingAddress Address `json:"visitingAddress" db:"va"`
	PostalAddress   Address `json:"postalAddress" db:"pa"`
	BillingAddress  Address `json:"billingAddress" db:"ba"`
} //@name Org
