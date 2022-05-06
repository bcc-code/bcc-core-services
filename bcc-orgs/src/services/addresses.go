package services

// import (
// 	"bcc-orgs/src/conversions"
// 	"bcc-orgs/src/models"
// 	"bcc-orgs/src/sqlc"
// 	"bcc-orgs/src/utils"
// 	"context"
// 	"database/sql"
// )

// type AddressType int

// const (
// 	VISITING_ADDRESS AddressType = iota
// 	POSTAL_ADDRESS
// 	BILLING_ADDRESS
// )

// func GetAddressesFromOrg(org models.Org) [3]models.Address {
// 	var addresses [3]models.Address
// 	addresses[VISITING_ADDRESS] = org.VisitingAddress
// 	addresses[POSTAL_ADDRESS] = org.PostalAddress
// 	addresses[BILLING_ADDRESS] = org.BillingAddress
// 	return addresses
// }
// func AddAddressIdsToCreateOrg(org *sqlc.CreateOrgParams, addressIds [3]int32) {
// 	org.FkVisitingAddressID = sql.NullInt32{Int32: addressIds[VISITING_ADDRESS], Valid: true}
// 	org.FkPostalAddressID = sql.NullInt32{Int32: addressIds[POSTAL_ADDRESS], Valid: true}
// 	org.FkBillingAddressID = sql.NullInt32{Int32: addressIds[BILLING_ADDRESS], Valid: true}
// }
// func GetAddressIdsFromDbOrg(org sqlc.Org) [3]int32 {
// 	var ids [3]int32
// 	ids[VISITING_ADDRESS] = org.FkVisitingAddressID.Int32
// 	ids[POSTAL_ADDRESS] = org.FkPostalAddressID.Int32
// 	ids[BILLING_ADDRESS] = org.FkBillingAddressID.Int32
// 	return ids
// }

// func CreateOrgAddresses(org models.Org) ([3]int32, error) {
// 	addresses := GetAddressesFromOrg(org)
// 	createdAddresses := [3]int32{}
// 	for i := AddressType(0); i < 3; i++ {
// 		addressCreate := conversions.ApiAddressToDbAddress[sqlc.CreateAddressParams](addresses[i])
// 		created, err := utils.SqlcDb.CreateAddress(context.Background(), addressCreate)
// 		createdAddresses[i] = created
// 		if err != nil {
// 			return createdAddresses, err
// 		}
// 	}
// 	return createdAddresses, nil
// }

// func UpdateOrgAddresses(org sqlc.Org, updateOrg models.Org) ([3]int32, error) {
// 	addressIds := GetAddressIdsFromDbOrg(org)
// 	updatedAddresses := GetAddressesFromOrg(updateOrg)
// 	var newAddressIds [3]int32
// 	for i := 0; i < 3; i++ {
// 		createdID, err := UpdateAddress(addressIds[i], updatedAddresses[i])
// 		newAddressIds[i] = createdID
// 		if err != nil {
// 			return newAddressIds, err
// 		}
// 	}
// 	return newAddressIds, nil
// }

// func UpdateAddress(addressID int32, address models.Address) (int32, error) {
// 	if addressID == 0 {
// 		return 0, nil
// 	}
// 	addressUpdate := conversions.ApiAddressToDbAddress[sqlc.UpdateAddressParams](address)
// 	return utils.SqlcDb.UpdateAddress(context.Background(), addressUpdate)
// }
