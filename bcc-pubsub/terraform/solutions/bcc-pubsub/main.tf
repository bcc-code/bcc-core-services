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
    key                  = "bcc-pubsub-app.terraform.tfstate"
  }
}

provider "azurerm" {
  features {}
}

provider "azapi" {
}

data "azurerm_resource_group" "envrg" {
  name     = "BCC-Platform-${terraform.workspace}"
}

data "azapi_resource" "env" {
  name      = "bcc-platform-${terraform.workspace}-env"
  parent_id = data.azurerm_resource_group.envrg.id
  type      = "Microsoft.App/managedEnvironments@2022-03-01"
}

resource "azurerm_resource_group" "rg" {
  name     =  "BCC-PubSub-${terraform.workspace}"
  location = var.location
}

module "container_app" {
  source                           = "../../modules/container_apps"
  managed_environment_id           = data.azapi_resource.env.id
  location                         = var.location
  resource_group_id                = azurerm_resource_group.rg.id
  tags                             = var.tags
  container_app                   = {
    name              = "bcc-pubsub-${terraform.workspace}"
    configuration      = {
      ingress          = {
        external       = true
        targetPort     = 80
      }
      dapr             = {
        enabled        = true
        appId          = "bcc-pubsub-${terraform.workspace}"
        appProtocol    = "http"
        appPort        = 80
      }
    }
    template          = {
      containers      = [{
        image         = "hello-world:latest"
        name          = "bcc-pubsub-dev"
        env           = [{
          name        = "APP_PORT"
          value       = 80
        }]
        resources     = {
          cpu         = 0.5
          memory      = "1Gi"
        }
      }]
      scale           = {
        minReplicas   = 0
        maxReplicas   = 1
      }
    }
  }
}

module "front_door_route" {
  name                 = "pubsub"
  source               = "../../modules/front_door_route"
  resource_group_id    = data.azurerm_resource_group.envrg.id
  origin_host          = module.container_app.domain_name   
  front_door_name      = "bcc-platform-${terraform.workspace}-frontdoor"
  endpoint_domain_name = "az-api-${terraform.workspace}.bcc.no"
  depends_on = [
    module.container_app
  ]
}