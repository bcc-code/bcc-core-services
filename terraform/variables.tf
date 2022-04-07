variable "gcp-project-id" {
  type = string
}

variable "gcp-location" {
  type = string
}

variable "google-credentials" {
  sensitive = true
  type      = string
}

variable "environment-name" {
  type = string
}

variable "repository-name" {
  type = string
}

variable "github-token" {
  sensitive = true
}
