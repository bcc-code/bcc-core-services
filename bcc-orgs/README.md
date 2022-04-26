# BCC Orgs API

## Application overview

The application is responsible for the following core APIs

1. Districts API
2. Orgs API
3. OrgAssociations API

## Running applicaiton

Copy ```.env.example``` to ```.env``` and fill in the values.

### Run locally

#### Without live-reload

1. Install [go](https://go.dev/doc/install)
2. From application root directory run
   ```go run src/main.go```

#### With live-reload

1. Install [go](https://go.dev/doc/install)
2. Install [air](https://github.com/cosmtrek/air)
3. From application root directory run
   ```air```

### Run in docker

1. Install [docker](https://docs.docker.com/get-docker/)
2. Install [docker-compose](https://docs.docker.com/compose/install/)
3. Run one of the following scripts:
   1. ```./scripts/run-dev-docker.sh``` for live-reload
   2. ```./scripts/run-prod-docker.sh``` for production-like image

## Interact with the application

1. Use REST Client extension with requests from ```requests.http```

## Develop application

The only officially supported development environment is VS Code. A Database management tool is included in the docker compose setup. Go to localhost:8080 and login with the settings in the .env to make use of this tool.

### Testing

To run tests locally you can attach a shell to the "orgs-api" container. In the container running the ```go test -v ./...``` command will run all files with a *_test.go naming. Running the tests in the CI/CD pipeline is a to do.

### Recomended extensions

1. [Go](https://marketplace.visualstudio.com/items?itemName=golang.Go)
2. [Go-Outliner](https://marketplace.visualstudio.com/items?itemName=766b.go-outliner)
3. [Go-Doc](https://marketplace.visualstudio.com/items?itemName=msyrus.go-doc)
4. [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client)

### Database migrations

#### Docker

1. When using ```./scripts/run-dev-docker.sh``` script the database is reset automatically.
2. Database reset can also be triggered by running ```./scripts/reset-db-docker.sh```

#### Locally

1. Install [goose](https://github.com/pressly/goose)
2. Run 
   ```bash
      goose -dir /db/migrations -table schema_migrations postgres "host=$POSTGRES_HOST port=$POSTGRES_PORT user=$POSTGRES_USER password=$POSTGRES_PASSWORD dbname=$POSTGRES_DB sslmode=disable" up
   ```
   With correct env vars

### Other tips

Add following snippet to your editor ```settings.json``` file
```json
"gopls": {
   "experimentalWorkspaceModule": true,
},
```
It will allow for correct module highlighting if the go.mod file is not in root directory

## Deployment

1. Production and staging environments are automatically deployed to Google Cloud Run on any commit to master that updates any file inside bcc-orgs directory.
2. Dev and staging environments can be manually deployed in github actions UI.
