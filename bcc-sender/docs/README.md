## BCC-SENDER

For now sender is hosted on cloud run.


- Api url:

https://bcc-sender-prod-km2ruurtvq-ew.a.run.app


- Swagger url:

https://bcc-sender-prod-km2ruurtvq-ew.a.run.app/swagger

### Auth

Add "x-access-token" with apiKey as header. To get apiKey contact with owner of this repo.

### Info
fromEmailAddress property is not used for now.

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

### Sample C# implementation

```C#
  var client = _factory.CreateClient();
  var message = new HttpRequestMessage
  {
      Method = HttpMethod.Post,
      RequestUri = new Uri("https://bcc-sender-prod-km2ruurtvq-ew.a.run.app/Mail")
  };
  message.Headers.Add("x-access-token", apiKey);

  var json = new MsFlowEmailData
  {
      ToEmailAddress = "test@bcc.no",
      Subject = "Test subject",
      Content = "Test",
      IsHtml = false,
      FromEmailAddress = "test@bcc.no",
      BccEmailsAddresses = new string[]
      {
        "test@bcc.no"
      },
      ReplyToEmailsAddresses = new[]
      {
        "test@bcc.no"
      }
  };

  message.Content = JsonContent.Create(json);
  HttpResponseMessage response = await client.SendAsync(message, new CancellationToken());;

```
