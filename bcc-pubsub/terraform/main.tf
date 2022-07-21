terraform {
  required_version = ">= 1.0"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "3.3.0"
    }
    azapi = {
      source  = "Azure/azapi"
      version = "0.4.0"
    }
  }
  experiments = [module_variable_optional_attrs]

  backend "azurerm" {
    resource_group_name  = "BCC-Platform"
    storage_account_name = "bccplatformtfstate"
    container_name       = "tfstate"
    key                  = "bcc-platform.terraform.tfstate"
  }
}

provider "azurerm" {
  features {}
}

provider "azapi" {
}

resource "random_string" "resource_prefix" {
  length  = 6
  special = false
  upper   = false
  numeric  = false
}



locals {
  resource_prefix = lower("${var.resource_group_name}-${terraform.workspace}")
}


resource "azurerm_container_registry" "acr" {
  name                = "bccplatform"
  resource_group_name = "BCC-Platform"
  location            = var.location
  sku                 = "Basic"
  admin_enabled       = true
}

resource "azurerm_resource_group" "rg" {
  name     = "${var.resource_group_name}-${terraform.workspace}"
  location = var.location
  tags     = var.tags
}

module "log_analytics_workspace" {
  source                           = "./modules/log_analytics"
  name                             = "${local.resource_prefix}-logs"
  location                         = var.location
  resource_group_name              = azurerm_resource_group.rg.name
  tags                             = var.tags
}

module "front_door" {
  source                           = "./modules/front_door"
  name                             = "${local.resource_prefix}-frontdoor"
  location                         = var.location
  resource_group_id                = azurerm_resource_group.rg.id
  tags                             = var.tags
  endpoint_domain_name             = "az-api-${terraform.workspace}.bcc.no"
}

module "application_insights" {
  source                           = "./modules/application_insights"
  name                             = "${local.resource_prefix}-env-insights"
  location                         = var.location
  resource_group_name              = azurerm_resource_group.rg.name
  tags                             = var.tags
  application_type                 = var.application_insights_application_type
  workspace_id                     = module.log_analytics_workspace.id
}

module "storage_account" {
  source                           = "./modules/storage_account"
  name                             = replace(lower("${local.resource_prefix}-logs"), "-","")
  location                         = var.location
  resource_group_name              = azurerm_resource_group.rg.name
  tags                             = var.tags
  account_kind                     = var.storage_account_kind
  account_tier                     = var.storage_account_tier
  replication_type                 = var.storage_account_replication_type
}

module "container_apps_vlan" {
  source                           = "./modules/container_apps_vlan"
  name                             = "${local.resource_prefix}-vlan"
  location                         = var.location
  resource_group_name                = azurerm_resource_group.rg.name
  tags                             = var.tags
}

module "container_apps_env"  {
  source                           = "./modules/container_apps_env"
  managed_environment_name         = "${local.resource_prefix}-env"
  location                         = var.location
  resource_group_id                = azurerm_resource_group.rg.id
  tags                             = var.tags
  instrumentation_key              = module.application_insights.instrumentation_key
  workspace_id                     = module.log_analytics_workspace.workspace_id
  primary_shared_key               = module.log_analytics_workspace.primary_shared_key
  vlan_subnet_id                   = module.container_apps_vlan.subnet_id
}


# module "container_apps" {
#   source                           = "./modules/container_app"
#   managed_environment_id           = module.container_apps_env.id
#   location                         = var.location
#   resource_group_id                = azurerm_resource_group.rg.id
#   tags                             = var.tags
#   dapr_components                  = [{
#                                       name            = var.dapr_component_name
#                                       componentType   = var.dapr_component_type
#                                       version         = var.dapr_component_version
#                                       ignoreErrors    = var.dapr_ignore_errors
#                                       initTimeout     = var.dapr_component_init_timeout
#                                       secrets         = [
#                                         {
#                                           name        = "storageaccountkey"
#                                           value       = module.storage_account.primary_access_key
#                                         }
#                                       ]
#                                       metadata: [
#                                         {
#                                           name        = "accountName"
#                                           value       = module.storage_account.name
#                                         },
#                                         {
#                                           name        = "containerName"
#                                           value       = var.container_name
#                                         },
#                                         {
#                                           name        = "accountKey"
#                                           secretRef   = "storageaccountkey"
#                                         }
#                                       ]
#                                       scopes          = var.dapr_component_scopes
#                                      }]
#   container_apps                   = var.container_apps
# }