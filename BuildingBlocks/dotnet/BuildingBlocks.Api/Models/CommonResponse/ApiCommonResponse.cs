using System.Net;

namespace BuildingBlocks.Api.Models.CommonResponse
{
    public class ApiCommonResponse<T> where T: class
    {
        private ApiCommonResponse()
        {
        }

        public T Data { get; internal set; }

        public bool HasErrors => Messages.Any(x => x.Type == ApiCommonResponseMessageType.Error);

        public bool HasWarnings => Messages.Any(x => x.Type == ApiCommonResponseMessageType.Warning);

        public ICollection<ApiCommonResponseMessage> Messages { get; internal set; }

        public HttpStatusCode Status { get; internal set; }

        public static IApiCommonResponseBuilderWithStatus Create()
        {
            var builder = new ApiCommonResponseBuilder(new ApiCommonResponse<T>());
            return builder;
        }
    }
}