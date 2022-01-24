## Following checklist need to be completed for all new features/modules

### CI/CD
- every new module should start with CI/CD for all available
- production ymls run only for push to master/main branch
- staging ymls run ad-hoc (workflow_dispatch)
- development ymls (for developers to test their feature) run on pull_request

### Workflow authentication

Current way of authentication with Google is shown below. There are 3 important steps in specific order to be able to authenticate.

1. Checkout code ```actions/checkout@v2```. This package must come first!
2. Use ```google-github-actions/auth@v0``` New package to authenticate: https://github.com/google-github-actions/auth
3. Use ```google-github-actions/setup-gcloud@v0```. Notice: all authentication inputs are deprecated: https://github.com/google-github-actions/setup-gcloud 

#### Example:
```
jobs:
  build_and_deploy:
    name: Build & Deploy
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2 # MUST COME FIRST!
      - name: Set up Cloud SDK
        uses: google-github-actions/auth@v0 
        with:
          project_id: ${{ env.GCP_PROJECT }}
          credentials_json: ${{ env.GCP_KEY }}

      - name: Set up Cloud SDK
        uses: google-github-actions/setup-gcloud@v0 

```

### Analytics
- all new modules should use at least Azure Application Insights

### Database migrations
- SQL scripts or DB Migrations need to be run on staging (some of the scripts might require longer executing time and causing fails)
- all yml files should be present for all available environments
