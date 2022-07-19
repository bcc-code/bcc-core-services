# https://github.com/hashicorp/terraform-provider-azurerm/issues/11206

RESOURCE_GROUP="bcc-platform-dev"
LOCATION="westeurope"
CONTAINERAPPS_ENVIRONMENT="bcc-platform-dev"
STORAGE_ACCOUNT_CONTAINER="bccplatformdev"

az storage account create \
  --name $STORAGE_ACCOUNT \
  --resource-group $RESOURCE_GROUP \
  --location "$LOCATION" \
  --sku Standard_RAGRS \
  --kind StorageV2