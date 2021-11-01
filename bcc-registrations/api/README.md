## Project Structure

### Bcc.Registrations 
- Core business logic implementation (domain layer)

### Bcc.Registrations.Api
- Host application
- Web API endpoints for service
- Handles authentication (not fine grained permissions)

### Bcc.Registrations.Contracts
- Public model and service contracts (interface definition)
- DTOs, Messages (commands, )