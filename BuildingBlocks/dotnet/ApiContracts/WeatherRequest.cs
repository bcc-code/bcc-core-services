using Microsoft.AspNetCore.Mvc;

namespace ApiContracts;

/// <summary>
/// Weather request
/// </summary>
public class WeatherRequest
{
    /// <summary>
    /// Provide Location
    /// </summary>
    [FromQuery(Name = "loc")]
    public string Location { get; set; }
}