data "google_compute_default_service_account" "default" {
}

resource "google_service_account" "github-build" {
  provider = google-beta

  account_id   = "github-build-${var.environment-name}"
  display_name = "Github build account for ${var.environment-name}"
}

resource "google_service_account_key" "github-build-key" {
  service_account_id = google_service_account.github-build.name
}

resource "google_artifact_registry_repository_iam_member" "test-iam" {
  provider = google-beta

  location   = google_artifact_registry_repository.my-repo.location
  repository = google_artifact_registry_repository.my-repo.name
  role       = "roles/artifactregistry.writer"
  member     = "serviceAccount:${google_service_account.github-build.email}"
}

resource "google_service_account_iam_member" "gce-default-account-iam" {
  service_account_id = data.google_compute_default_service_account.default.name
  role               = "roles/iam.serviceAccountUser"
  member             = "serviceAccount:${google_service_account.github-build.email}"
}
