![image](https://user-images.githubusercontent.com/735432/158808996-373f755f-f476-49e7-b618-52e619154d37.png)

```
title Get access to an application's tenant / team

User->Application: SourceOrganisationId (ChurchId)
Application->TenantAPI: get all possible tenants for the user
TenantAPI->Application: application tenants array
Application->Application: check if requested tenant exists in the returned array
Application->User: allowed, return requested page
```
