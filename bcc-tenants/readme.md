https://www.websequencediagrams.com/
```
title Get access to an application's tenant / team

User->Application: a user enters an application
Application->TenantAPI: get tenant id for user's organisation
TenantAPI:
Application: Organization Ids a user has membership in
TenantAPI->Application: tenants array
Application->Application: application specific logic for tenants
Application->User: allowed, return requested page
```
