## Following checklist need to be completed for all new features

### CI/CD
- every new module should start with CI/CD for all available
- production ymls run only for push to master/main branch
- staging ymls run ad-hoc (workflow_dispatch)
- development ymls (for developers to test their feature) run on pull_request

### Analytics
- all new modules should use at least Azure Application Insights

### Database migrations
- SQL scripts or DB Migrations need to be run on staging (some of the scripts might require longer executing time and causing fails)
- all yml files should be present for all available environments
