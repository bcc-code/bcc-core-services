using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BuildingBlocks.Api.OpenApi;


public class QueryableParameters : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = ParameterNames.Limit,
            In = ParameterLocation.Query,
            Schema = new OpenApiSchema {Type = "number"},
            Required = false,
            AllowEmptyValue = true,
            Description = "Limit the number of returned records. Select all by setting to -1 (default: -1)"
        });
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = ParameterNames.Offset,
            In = ParameterLocation.Query,
            Schema = new OpenApiSchema {Type = "number"},
            Required = false,
            AllowEmptyValue = true,
            Description = "Skip the first n items in the response (default: 0)"
        });
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = ParameterNames.Page,
            In = ParameterLocation.Query,
            Schema = new OpenApiSchema {Type = "number"},
            Required = false,
            AllowEmptyValue = true,
            Description = @"An alternative to offset. Page is a way to set offset under the hood by calculating limit * (page - 1). 
                            If both offset and page are specified their effects will be added together (default: 1)"
        });
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = ParameterNames.Filter,
            In = ParameterLocation.Query,
            Schema = new OpenApiSchema {Type = "string"},
            Required = false,
            AllowEmptyValue = true,
            Description = @"Filter the results as described in directus json format
                            Allowed filter fields: age, brithDate, diseasedDate, displayName, email, firstName, middleName, gender, personID
                            Allowed operations _eq, _ne, _gt, _gte, _lt, _lte (default: empty)"
        });
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = ParameterNames.Fields,
            In = ParameterLocation.Query,
            Schema = new OpenApiSchema {Type = "string"},
            Required = false,
            AllowEmptyValue = true,
            Description = @"Comma delimited list of fields to return. Leave empty for all fields. Supports * as a wildcard (default: empty)
                            Example: ?fields=displayName,currentAddress.*"
        });
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = ParameterNames.Sort,
            In = ParameterLocation.Query,
            Schema = new OpenApiSchema {Type = "string"},
            Required = false,
            AllowEmptyValue = true,
            Description = @"Comma delimited list of fields to sort by as described in directus, without ? support.
                            Allowed sort fields: <same as for filter>.
                            Example: ?sort=displayName,-age"
        });
            
    }
}

static class ParameterNames
{
    public const string Limit = "limit";
    public const string Offset = "offset";
    public const string Page = "page";
    public const string Filter = "filter";
    public const string Fields = "fields";
    public const string Sort = "sort";
}