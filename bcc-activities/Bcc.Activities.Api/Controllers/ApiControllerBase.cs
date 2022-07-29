using System.Net;
using BuildingBlocks.Api.OpenApi.CommonResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bcc.Activities.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    public abstract class ApiControllerBase : Controller
    {
        protected virtual IActionResult CommonResponse(ApiCommonResponse result)
        {
            var response = new
            {
                status = result.Status.ToString(),
                statusCode = result.Status,
                data = result.Data,
                meta = result.Meta,
                messages = result.Messages?.Select(x => new
                {
                    Type = x.Type.ToString(),
                    x.Text
                })
            };

            if (result.HasErrors || result.Status == HttpStatusCode.BadRequest) return BadRequest(response);

            return Ok(response);
        }
        
        protected virtual IActionResult CommonResponse<T>(ApiCommonResponse<T> result) where T: class
        {
            var response = new
            {
                status = result.Status.ToString(),
                statusCode = result.Status,
                data = result.Data,
                meta = result.Meta,
                messages = result.Messages?.Select(x => new
                {
                    Type = x.Type.ToString(),
                    x.Text
                })
            };

            if (result.HasErrors || result.Status == HttpStatusCode.BadRequest) return BadRequest(response);

            return Ok(response);
        }
        
        
    }
}