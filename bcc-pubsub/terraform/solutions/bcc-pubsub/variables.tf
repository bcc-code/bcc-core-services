
variable "location" {
  description = "(Required) Specifies the supported Azure location where the resource exists. Changing this forces a new resource to be created."
  type        = string
  default     = "WestEurope"
}

variable "resource_group_name" {
   description = "Name of the resource group in which the resources will be created"
   default     = "BCC-Platform"
}

variable "tags" {
  description = "(Optional) Specifies tags for all the resources"
  default     = {
    createdWith = "Terraform"
  }
}

