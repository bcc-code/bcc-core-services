using BuildingBlocks.Api.Logging;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogService _logService;
        private readonly bool _isDebugMode;

        public ExceptionMiddleware(RequestDelegate next, ILogService logService, bool isDebugMode)
        {
            _next = next;
            _logService = logService;
            _isDebugMode = isDebugMode;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                _logService.TrackException(exception);

                if (_isDebugMode)
                {
                    throw;
                }

                throw new Exception("An exception occured");
            }
        }
    }
}