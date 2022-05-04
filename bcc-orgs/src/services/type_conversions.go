package services

import "bcc-orgs/src/models"

func DbOrgToApiOrg(org models.DbOrg) models.ApiOrg {
	return models.ApiOrg{
		OrgID:           org.OrgID,
		Name:            org.Name,
		LegalName:       org.LegalName,
		Type:            org.Type,
		VisitingAddress: DbAddressToApiAddress(org.VisitingAddress),
		BillingAddress:  DbAddressToApiAddress(org.BillingAddress),
		PostalAddress:   DbAddressToApiAddress(org.PostalAddress),
	}
}

func DbAddressToApiAddress(address *models.DbAddress) *models.ApiAddress {
	if address == nil {
		return nil
	}
	return &models.ApiAddress{
		Street1:           address.Street1,
		Street2:           address.Street2,
		City:              address.City,
		Region:            address.Region,
		CountryIso2Code:   address.CountryIso2Code,
		PostalCode:        address.PostalCode,
		CountryName:       address.CountryName,
		CountryNameNative: address.CountryNameNative,
	}
}

func ApiAddressToDbAddress(address *models.ApiAddress) *models.DbAddress {
	if address == nil {
		return nil
	}
	return &models.DbAddress{
		Street1:           address.Street1,
		Street2:           address.Street2,
		City:              address.City,
		Region:            address.Region,
		CountryIso2Code:   address.CountryIso2Code,
		PostalCode:        address.PostalCode,
		CountryName:       address.CountryName,
		CountryNameNative: address.CountryNameNative,
	}
}

func ApiOrgCreateToDbOrg(org models.ApiOrgCreate) models.DbOrg {
	return models.DbOrg{
		Name:            org.Name,
		LegalName:       org.LegalName,
		Type:            org.Type,
		VisitingAddress: ApiAddressToDbAddress(org.VisitingAddress),
		BillingAddress:  ApiAddressToDbAddress(org.BillingAddress),
		PostalAddress:   ApiAddressToDbAddress(org.PostalAddress),
	}
}

func ApiOrgUpdateToDbOrgUpdate(org models.ApiOrgUpdate) models.DbOrgUpdate {
	return models.DbOrgUpdate{
		Name:            org.Name,
		LegalName:       org.LegalName,
		Type:            org.Type,
		VisitingAddress: ApiAddressUpdateToDbAddressUpdate(org.VisitingAddress),
		BillingAddress:  ApiAddressUpdateToDbAddressUpdate(org.BillingAddress),
		PostalAddress:   ApiAddressUpdateToDbAddressUpdate(org.PostalAddress),
	}
}

func ApiAddressUpdateToDbAddressUpdate(address *models.ApiAddressUpdate) *models.DbAddressUpdate {
	if address == nil {
		return nil
	}
	dbAddress := models.DbAddressUpdate(*address)
	return &dbAddress
}
