# Registration Service

## Resources

  - Registrations (Each person may have one registration per activity)
  - ActivityRegistrationConfig (The registration settings regarding an activity)
  - Conditions (Rules and their result logic that are checked and applied per registration)
  - Rules (Checkable logic eg. `Is older than 12 years of age` becomes `profile.age > 12`)
  - Forms (Customizable forms to request extra information upon registration)
  - Responses (One response per registration per form)

## Definitions

### Profile
    {
        personId,
        firstName,
        lastName,
        age,
        gender,
        teamId
    }

### Condition 
    {
        _result: {
            field: 'canRegister'
            value: 'allow'|'deny'
        },
        _and: {
            ruleA,
            _or: {
                ruleB,
                ruleC
            }
        }
    }

Translated into code this could look like:
```
state = { canRegister: 'deny' }
if (ruleA(profile) && (ruleB(profile) || ruleC(profile))) {
    state['canRegister'] = 'allow'
}
```

## API

THe entire api supports multi-tenancy, meaning each request is scope by the tenant making the request

- ### Registrations
  - CRUD
    - CREATE Registration(activityId, profile)
    - READ Registration(id)
    - LIST Registration(activityId, [teamId, profile])
    - UPDATE Registration(id, payload)
    - DELETE Registration(id)
  - Other
    - CanRegister(profile) - Retrieve all activities this person can register for


- ### ActivityRegistrationConfig
    - CRUD
        - CREATE ActivityRegistrationConfig(activityId, payload)
        - READ ActivityRegistrationConfig(activityId)
        - UPDATE ActivityRegistrationConfig(activityId, payload)
        - DELETE ActivityRegistrationConfig(activityId)
    - Other
        - CanRegister(profile) - Retrieve all activities this person can register for

- ### Conditions
  - eg. `if (olderThan12years(profile) && youngerThan36years(profile)) then allow` 
  - CRUD
      - CREATE Condition(payload)
      - READ Condition(id)
      - LIST Conditions(activityId)
      - UPDATE Condition(id, payload)
      - DELETE Condition(id)
