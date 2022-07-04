using System.Net;

namespace BuildingBlocks.Api.Models.CommonResponse
{
    public class ApiCommonResponse
    {
        protected ApiCommonResponse()
        {
        }

        public object Data { get; internal set; }

        public bool HasErrors => Messages.Any(x => x.Type == ApiCommonResponseMessageType.Error);

        public bool HasWarnings => Messages.Any(x => x.Type == ApiCommonResponseMessageType.Warning);

        public ICollection<ApiCommonResponseMessage> Messages { get; internal set; }

        public HttpStatusCode Status { get; internal set; }

        public static IApiCommonResponseBuilderWithStatus Create()
        {
            var builder = new ApiCommonResponseBuilder(new ApiCommonResponse());
            return builder;
        }
    }
    // Used for SwaggerGen
    public class ApiCommonResponse<T> : ApiCommonResponse
    {
        public new T Data { get; }

        public ApiCommonResponse(T data)
        {
            Data = data;
        }
        
    }
}