#!/bin/bash

env_name="dev"

# Select dev workspace
terraform workspace select $env_name || terraform workspace new $env_name

# Terraform Init
terraform init

# Terraform validate
terraform validate -compact-warnings

# Terraform plan
terraform plan -var-file=deploy-$env_name.tfvars -out=main-$env_name.tfplan 

# Terraform apply
terraform apply -compact-warnings -auto-approve main-$env_name.tfplan

#
terraform untaint module.front_door_route.azapi_resource.route