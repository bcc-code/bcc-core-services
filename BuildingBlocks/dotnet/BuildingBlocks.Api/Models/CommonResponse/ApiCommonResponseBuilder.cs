using System.Net;

namespace BuildingBlocks.Api.Models.CommonResponse
{
    public class ApiCommonResponseBuilder : Builder<ApiCommonResponse>,
        IApiCommonResponseBuilderWithStatus,
        IApiCommonResponseBuilderWithMessage
    {
        internal ApiCommonResponseBuilder(ApiCommonResponse apiCommonResponse) : base(apiCommonResponse)
        {
        }

        public Builder<ApiCommonResponse> WithData(object data)
        {
            Target.Data = data;
            return this;
        }

        public IApiCommonResponseBuilderWithData WithMessage(ApiCommonResponseMessage message)
        {
            Target.Messages ??= new List<ApiCommonResponseMessage>();

            Target.Messages.Add(message);

            return this;
        }

        public IApiCommonResponseBuilderWithMessage WithFailure()
        {
            Target.Status = HttpStatusCode.BadRequest;
            return this;
        }

        public IApiCommonResponseBuilderWithMessage WithFailure(IEnumerable<string> errors)
        {
            WithFailure();

            Target.Messages ??= new List<ApiCommonResponseMessage>();

            foreach (var error in errors)
                Target.Messages.Add(new ApiCommonResponseMessage(error, ApiCommonResponseMessageType.Error));

            return this;
        }

        public IApiCommonResponseBuilderWithMessage WithFailure(string error)
        {
            WithFailure(new List<string> {error});

            return this;
        }

        public IApiCommonResponseBuilderWithMessage WithSuccess()
        {
            Target.Status = HttpStatusCode.OK;

            return this;
        }
        public IApiCommonResponseBuilderWithMessage WithStatus(HttpStatusCode statusCode)
        {
            Target.Status = statusCode;

            return this;
        }
    }

    public interface IApiCommonResponseBuilderWithData : IBuilder<ApiCommonResponse>
    {
        Builder<ApiCommonResponse> WithData(object data);
    }

    public interface IApiCommonResponseBuilderWithStatus
    {
        IApiCommonResponseBuilderWithMessage WithFailure();
        IApiCommonResponseBuilderWithMessage WithFailure(string error);
        IApiCommonResponseBuilderWithMessage WithFailure(IEnumerable<string> errors);
        IApiCommonResponseBuilderWithMessage WithSuccess();
        
        IApiCommonResponseBuilderWithMessage WithStatus(HttpStatusCode statusCode);
    }

    public interface IApiCommonResponseBuilderWithMessage : IApiCommonResponseBuilderWithData
    {
        IApiCommonResponseBuilderWithData WithMessage(ApiCommonResponseMessage message);
    }
}