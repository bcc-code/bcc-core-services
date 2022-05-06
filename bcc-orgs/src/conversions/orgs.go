package conversions

// import (
// 	"bcc-orgs/src/dbModels"
// 	"bcc-orgs/src/models"
// 	"bcc-orgs/src/sqlc"
// 	"database/sql"
// )

// func DbOrgToApiOrg(o dbModels.Org) models.Org {
// 	return models.Org{
// 		OrgID:           int(o.OrgID),
// 		Name:            o.Name,
// 		LegalName:       &o.LegalName.String,
// 		Type:            models.OrgType((o.Type)),
// 		VisitingAddress: DbAddressToApiAddress(o.VisitingAddress),
// 		PostalAddress:   DbAddressToApiAddress(o.PostalAddress),
// 		BillingAddress:  DbAddressToApiAddress(o.BillingAddress),
// 	}
// }

// func ApiOrgToDbOrgCreate(o models.Org) sqlc.CreateOrgParams {
// 	return sqlc.CreateOrgParams{
// 		Name:      o.Name,
// 		LegalName: sql.NullString{String: *o.LegalName, Valid: true},
// 		Type:      string(o.Type),
// 	}
// }
