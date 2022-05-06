package models

type Org struct {
	OrgID int `json:"orgID" extensions:"x-order=1"`
	OrgCreate
} //@name Org

type OrgCreate struct {
	Name            *string  `json:"name" extensions:"x-order=2"`
	LegalName       *string  `json:"legalName" extensions:"x-order=3"`
	Type            *OrgType `json:"type" extensions:"x-order=4"`
	VisitingAddress Address  `json:"visitingAddress" extensions:"x-order=5"`
	PostalAddress   Address  `json:"postalAddress"  extensions:"x-order=6"`
	BillingAddress  Address  `json:"billingAddress" extensions:"x-order=7"`
} //@name OrgCreate

type OrgType string

const (
	CHURCH OrgType = "church"
	ORG            = "org"
	CLUB           = "club"
)
