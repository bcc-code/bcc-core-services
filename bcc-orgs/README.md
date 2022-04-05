# BCC Orgs API

## Application overview

The application is responsible for the following core APIs

1. Districts API
2. Orgs API
3. OrgAssociations API

## Running applicaiton

Copy ```.env.example``` to ```.env```

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

## Develop application

The only officially supported development environment is VS Code

### Recomended extensions

1. [Go] (https://marketplace.visualstudio.com/items?itemName=golang.Go)
2. [Go-Outliner] (https://marketplace.visualstudio.com/items?itemName=766b.go-outliner)
3. [Go-Doc] (https://marketplace.visualstudio.com/items?itemName=msyrus.go-doc)

### Other tips

Add following snippet to your editor ```settings.json``` file
```
"gopls": {
   "experimentalWorkspaceModule": true,
},
```
It will allow for correct module highlighting if the go.mod file is not in root directory

## Deployment

Application is automatically deployed to Google Cloud Run on any commit to master that updates any file inside bcc-orgs directory
