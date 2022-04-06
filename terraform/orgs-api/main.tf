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

data "google_iam_policy" "noauth" {
  binding {
    role = "roles/run.invoker"
    members = [
      "allUsers",
    ]
  }
}

resource "google_cloud_run_service_iam_policy" "noauth" {
  location    = google_cloud_run_service.default.location
  project     = google_cloud_run_service.default.project
  service     = google_cloud_run_service.default.name

  policy_data = data.google_iam_policy.noauth.policy_data
}

resource "google_cloud_run_service_iam_member" "member" {
  location = google_cloud_run_service.default.location
  service = google_cloud_run_service.default.name
  role = "roles/owner"
  member = "serviceAccount:${var.service-account-email}"
}