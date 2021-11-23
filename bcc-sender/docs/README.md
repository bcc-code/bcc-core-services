## BCC-SENDER

For now sender is hosted on cloud run:
https://bcc-sender-prod-km2ruurtvq-ew.a.run.app/swagger

### Auth

Add "x-access-token" with apiKey as header

### [POST] /Mail - sending email with MS Flow

body:

```json
{
  "toEmailAddress": "string",
  "subject": "string",
  "content": "string",
  "isHtml": true,
  "fromEmailAddress": "string",
  "bccEmailsAddresses": ["string"],
  "replyToEmailsAddresses": ["string"]
}
```

### [POST] /Sms - sending SMS with Twilio

body:

```json
{
  "number": "string",
  "content": "string"
}
```
