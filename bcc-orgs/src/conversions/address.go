package conversions

// import (
// 	"bcc-orgs/src/models"
// 	"bcc-orgs/src/sqlc"
// )

// func DbAddressToApiAddress(a sqlc.Address) models.Address {
// 	return models.Address{
// 		Street1:           &a.Street1.String,
// 		Street2:           &a.Street2.String,
// 		City:              &a.City.String,
// 		Region:            &a.Region.String,
// 		CountryIso2Code:   &a.CountryIso2Code.String,
// 		PostalCode:        &a.PostalCode.String,
// 		CountryName:       &a.CountryName.String,
// 		CountryNameNative: &a.CountryNameNative.String,
// 	}
// }

// func ApiAddressToDbAddress[addrType sqlc.UpdateAddressParams | sqlc.CreateAddressParams](a models.Address) addrType {
// 	myAddr := addrType{}

// 	return myAddr
// }
