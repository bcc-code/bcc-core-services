
resource "google_artifact_registry_repository" "my-repo" {
  provider = google-beta

  location      = var.gcp-location
  repository_id = "default-${var.environment-name}"
  description   = "Default repository for ${var.environment-name}"
  format        = "DOCKER"
}