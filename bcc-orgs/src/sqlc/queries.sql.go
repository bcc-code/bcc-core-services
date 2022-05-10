// Code generated by sqlc. DO NOT EDIT.
// versions:
//   sqlc v1.13.0
// source: queries.sql

package sqlc

import (
	"context"
	"database/sql"
)

const getNewOrgs = `-- name: GetNewOrgs :one
    SELECT 
        org_id,
        (visiting_address).street_1
    FROM new_org
`

type GetNewOrgsRow struct {
	OrgID   int32       `db:"org_id"`
	Column2 interface{} `db:"column_2"`
}

func (q *Queries) GetNewOrgs(ctx context.Context) (GetNewOrgsRow, error) {
	row := q.db.QueryRowContext(ctx, getNewOrgs)
	var i GetNewOrgsRow
	err := row.Scan(&i.OrgID, &i.Column2)
	return i, err
}

const getOrgs = `-- name: GetOrgs :one
    SELECT o.org_id, o.name, o.legal_name, o.type, o.fk_visiting_address_id, o.fk_postal_address_id, o.fk_billing_address_id, a::INT as va
    FROM org as o LEFT JOIN address as a ON o.visiting_address_id = a.address_id
`

type GetOrgsRow struct {
	OrgID               int32          `db:"org_id"`
	Name                string         `db:"name"`
	LegalName           sql.NullString `db:"legal_name"`
	Type                string         `db:"type"`
	FkVisitingAddressID sql.NullInt32  `db:"fk_visiting_address_id"`
	FkPostalAddressID   sql.NullInt32  `db:"fk_postal_address_id"`
	FkBillingAddressID  sql.NullInt32  `db:"fk_billing_address_id"`
	Va                  int32          `db:"va"`
}

func (q *Queries) GetOrgs(ctx context.Context) (GetOrgsRow, error) {
	row := q.db.QueryRowContext(ctx, getOrgs)
	var i GetOrgsRow
	err := row.Scan(
		&i.OrgID,
		&i.Name,
		&i.LegalName,
		&i.Type,
		&i.FkVisitingAddressID,
		&i.FkPostalAddressID,
		&i.FkBillingAddressID,
		&i.Va,
	)
	return i, err
}
