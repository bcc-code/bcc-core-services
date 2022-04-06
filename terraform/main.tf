terraform {
  required_providers {
    google = {
      source = "hashicorp/google"
    }
  }
}

provider "google-beta" {
  credentials = var.google-credentials

  project = var.gcp-project-id
  region  = var.gcp-location
}

provider "google" {
  credentials = var.google-credentials

  project = var.gcp-project-id
  region  = var.gcp-location
}

module "orgs-api" {
  source                = "./orgs-api"
  gcp-location          = var.gcp-location
  environment-name      = var.environment-name
  service-account-email = google_service_account.github-build.email
}
