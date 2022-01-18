Example repository:
https://github.com/piotrczyz/mass-transit-examples

# Conventions & best practices 
- queue name: a service/module name with `-queue` suffix (`myshare_contributions_service-queue`, `collection_service-queue`) 
- public events for a service/module should be available as contracts, a nuget package available on [BCC Code nuget.org account](https://www.nuget.org/profiles/bcc-code) (to be discussed)

# Retry strategy
**If an event fails** - consumer takes care of handling those exceptions/errors.

**If a command fails** - publisher needs to listen to an error queue in order to resend or do other stuff.

Ref.
In case of we use RabbitMQ: 
https://masstransit-project.com/usage/transports/rabbitmq.html#guidance
