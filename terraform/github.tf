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
  plaintext_value = base64decode(google_service_account_key.github-build-key.private_key)
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
  plaintext_value = "${google_artifact_registry_repository.default.location}-docker.pkg.dev"
}
resource "github_actions_environment_secret" "registry-name" {
  repository      = github_repository_environment.default.repository
  environment     = github_repository_environment.default.environment
  secret_name     = "REGISTRY_NAME"
  plaintext_value = google_artifact_registry_repository.default.name
}
resource "github_actions_environment_secret" "org-service-name" {
  repository      = github_repository_environment.default.repository
  environment     = github_repository_environment.default.environment 
  secret_name     = "ORG_SERVICE_NAME"
  plaintext_value = module.orgs-api.service.name
}
resource "github_actions_environment_secret" "service-region" {
  repository      = github_repository_environment.default.repository
  environment     = github_repository_environment.default.environment
  secret_name     = "ORG_SERVICE_REGION"
  plaintext_value = module.orgs-api.service.location
}
