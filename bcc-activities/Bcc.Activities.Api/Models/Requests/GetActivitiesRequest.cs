using Microsoft.AspNetCore.Mvc;

namespace Bcc.Activities.Api.Models.Requests;

/// <summary>
/// Get Activities request
/// </summary>
public class GetActivitiesRequest
{
    /// <summary>
    /// Limit the number of returned records. Select all by setting to -1 (default: -1)
    /// </summary>
    [FromQuery(Name = "limit")]
    public int Limit { get; set; }
    
    /// <summary>
    /// Skip the first n items in the response (default: 0)
    /// </summary>
    [FromQuery(Name = "offset")]
    public int Offset { get; set; }
    
    /// <summary>
    /// An alternative to offset. Page is a way to set offset under the hood by calculating ```limit * (page - 1)```.<br/>
    /// If both offset and page are specified their effects will be added together (default: 1)
    /// </summary>
    [FromQuery(Name = "page")]
    public int Page { get; set; }
    
    /// <summary>
    /// Filter the results as described in [directus](https://docs.directus.io/reference/query/#filter) json format<br/>
    /// Allowed filter fields: ```startDateTimeUtc, endDateTimeUtc, name, referenceNumber, description, location,
    ///                        lastChangedTimeUtc, createdById, responsibleId, responsibleName, neededParticipants```<br/>
    /// Allowed operations ```_eq, _ne, _gt, _gte, _lt, _lte``` (default: empty)
    /// </summary>
    [FromQuery(Name = "filter")]
    public string Filter { get; set; }
    
    // /// <summary>
    // /// Comma delimited list of fields to return. Leave empty for all fields.<br/>
    // /// Supports * as a wildcard (default: empty) <br/>
    // /// Example: ```?fields=displayName,currentAddress.* ```
    // /// </summary>
    // [FromQuery(Name = "fields")]
    // public string Fields { get; set; }
    
    /// <summary>
    /// Comma delimited list of fields to sort by as described in directus, without ? support.<br/>
    /// Allowed sort fields: ```startDateTimeUtc, endDateTimeUtc, name, referenceNumber, location, lastChangedTimeUtc, responsibleName, neededParticipants```. <br/>
    /// Example: ```?sort=startDateTimeUtc,name```
    /// </summary>
    [FromQuery(Name = "sort")]
    public string Sort { get; set; }
}