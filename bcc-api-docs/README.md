# BCC API Documentation APP

## Overview

The application is responsible for combining multiple openAPI specs into a single UI.
Additionally application provides an endpoint (docs/token) that makes it possible to retrieve auth token in the client_credentials flow from auth0.

## Running application

1. Copy ```.env.example``` to ```.env``` and fill in the values.
2. Run ```npm run dev```

## Interact with the application

Go to [http://localhost:4000/docs](http://localhost:4000/docs) to see the UI.

## Developing

1. To add a new API to the application, you need to update the terraform configuration in [BCC Infrastructure Repo](https://github.com/bcc-code/bcc-core-infra), adding an URL to your yaml file.
2. Optionally you can paste your API spec into the ```/specs``` folder, then you can reference it in the Terraform as ```/docs/<name>```

## Deployment

1. Production and staging environments are automatically deployed to Google Cloud Run on any commit to master that updates any file inside bcc-api-docs directory.
2. Dev and staging environments can be manually deployed in github actions UI.
