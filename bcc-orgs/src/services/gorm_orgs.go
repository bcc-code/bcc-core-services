package services

import (
	"bcc-orgs/src/models"
	"bcc-orgs/src/utils"

	"github.com/samber/lo"
)

func GormFindOrgs() ([]models.ApiOrg, error) {
	var dbOrgs []models.DbOrg
	utils.GormDb.Joins("VisitingAddress").Joins("BillingAddress").Joins("PostalAddress").Find(&dbOrgs)
	orgs := lo.Map(dbOrgs, func(o models.DbOrg, _ int) models.ApiOrg {
		return DbOrgToApiOrg(o)
	})
	return orgs, nil
}

func GormGetOrg(orgID int) (models.ApiOrg, error) {
	var dbOrg models.DbOrg
	utils.GormDb.Joins("VisitingAddress").Joins("BillingAddress").Joins("PostalAddress").First(&dbOrg)
	return DbOrgToApiOrg(dbOrg), nil
}

func GormCreateOrg(org models.ApiOrgCreate) (models.ApiOrg, error) {
	dbOrg := ApiOrgCreateToDbOrg(org)
	utils.GormDb.Create(&dbOrg)
	return DbOrgToApiOrg(dbOrg), nil
}

func GormUpdateOrg(orgID int, org models.ApiOrgUpdate) (models.ApiOrg, error) {
	dbOrg := ApiOrgUpdateToDbOrgUpdate(org)
	resultOrg := models.DbOrg{}
	utils.GormDb.Model(&resultOrg).Where("org_id = ?", orgID).Updates(&dbOrg).Scan(&resultOrg)
	return DbOrgToApiOrg(resultOrg), nil
}
