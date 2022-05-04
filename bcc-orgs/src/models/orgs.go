package models

import "database/sql"

type ApiOrgCreate struct {
	Name            string      `json:"name"`
	LegalName       string      `json:"legalName"`
	Type            string      `json:"type"`
	VisitingAddress *ApiAddress `json:"visitingAddress"`
	PostalAddress   *ApiAddress `json:"postalAddress"`
	BillingAddress  *ApiAddress `json:"billingAddress"`
}

type ApiOrgUpdate struct {
	Name            *string           `json:"name"`
	LegalName       *string           `json:"legalName"`
	Type            *string           `json:"type"`
	VisitingAddress *ApiAddressUpdate `json:"visitingAddress"`
	PostalAddress   *ApiAddressUpdate `json:"postalAddress"`
	BillingAddress  *ApiAddressUpdate `json:"billingAddress"`
}

type ApiOrg struct {
	OrgID           int         `json:"orgID"`
	Name            string      `json:"name"`
	LegalName       string      `json:"legalName"`
	Type            string      `json:"type"`
	VisitingAddress *ApiAddress `json:"visitingAddress"`
	PostalAddress   *ApiAddress `json:"postalAddress"`
	BillingAddress  *ApiAddress `json:"billingAddress"`
}

type DbOrg struct {
	OrgID             int `gorm:"autoincrement;primary_key"`
	Name              string
	LegalName         string
	Type              string
	VisitingAddressID sql.NullInt64 `gorm:"column:fk_visiting_address_id"`
	PostalAddressID   sql.NullInt64 `gorm:"column:fk_postal_address_id"`
	BillingAddressID  sql.NullInt64 `gorm:"column:fk_billing_address_id"`
	VisitingAddress   *DbAddress    `gorm:"foreignkey:VisitingAddressID"`
	PostalAddress     *DbAddress    `gorm:"foreignkey:PostalAddressID"`
	BillingAddress    *DbAddress    `gorm:"foreignkey:BillingAddressID"`
}

type DbOrgUpdate struct {
	Name            *string
	LegalName       *string
	Type            *string
	VisitingAddress *DbAddressUpdate
	PostalAddress   *DbAddressUpdate
	BillingAddress  *DbAddressUpdate
}

func (DbOrg) TableName() string {
	return "org"
}
