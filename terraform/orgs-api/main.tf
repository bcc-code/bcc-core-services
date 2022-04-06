resource "google_cloud_run_service" "default" {
  name     = "org-api-${var.environment-name}"
  location = var.gcp-location

  template {
    spec {
      containers {
        image = "us-docker.pkg.dev/cloudrun/container/hello"
      }
    }
  }

  traffic {
    percent         = 100
    latest_revision = true
  }
}

resource "google_cloud_run_service_iam_member" "member" {
  location = google_cloud_run_service.default.location
  service = google_cloud_run_service.default.name
  role = "roles/owner"
  member = "serviceAccount:${var.service-account-email}"
}