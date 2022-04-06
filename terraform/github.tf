provider "github" {
  token = var.github-token
}

data "github_repository" "repo" {
  full_name = var.repository-name
}

resource "github_repository_environment" "default" {
  environment = var.environment-name
  repository  = "../${data.github_repository.repo.full_name}"
}

resource "github_actions_environment_secret" "google-credentials" {
  repository      = github_repository_environment.default.repository
  environment     = github_repository_environment.default.environment
  secret_name     = "GOOGLE_CREDENTIALS"
  plaintext_value = var.gcp-project-id
}

resource "github_actions_environment_secret" "project-id" {
  repository      = github_repository_environment.default.repository
  environment     = github_repository_environment.default.environment
  secret_name     = "PROJECT_ID"
  plaintext_value = var.gcp-project-id
}

resource "github_actions_environment_secret" "registry-base-url" {
  repository      = github_repository_environment.default.repository
  environment     = github_repository_environment.default.environment
  secret_name     = "REGISTRY_BASE_URL"
  plaintext_value = var.gcp-project-id
}
resource "github_actions_environment_secret" "registry-name" {
  repository      = github_repository_environment.default.repository
  environment     = github_repository_environment.default.environment
  secret_name     = "REGISTRY_NAME"
  plaintext_value = var.gcp-project-id
}
resource "github_actions_environment_secret" "org-service-name" {
  repository      = github_repository_environment.default.repository
  environment     = github_repository_environment.default.environment 
  secret_name     = "ORG_SERVICE_NAME"
  plaintext_value = orgs-api.google_cloud_run_service.default.name
}
resource "github_actions_environment_secret" "service-region" {
  repository      = github_repository_environment.default.repository
  environment     = github_repository_environment.default.environment
  secret_name     = "SERVICE_REGION"
  plaintext_value = var.gcp-project-id 
}
