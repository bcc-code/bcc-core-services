using System.Net;

namespace BuildingBlocks.Api.OpenApi.CommonResponse
{
    public class ApiCommonResponse
    {
        protected ApiCommonResponse()
        {
        }

        public object Data { get; internal set; }
        public Metadata Meta { get; internal set; }

        public bool HasErrors => Messages.Any(x => x.Type == ApiCommonResponseMessageType.Error);

        public bool HasWarnings =>
            Messages.Any(x => x.Type == ApiCommonResponseMessageType.Warning);

        public ICollection<ApiCommonResponseMessage> Messages { get; internal set; } =
            new List<ApiCommonResponseMessage>();

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