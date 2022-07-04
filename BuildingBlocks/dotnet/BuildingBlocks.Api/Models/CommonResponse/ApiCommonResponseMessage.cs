namespace BuildingBlocks.Api.Models.CommonResponse
{
    public class ApiCommonResponseMessage
    {
        public ApiCommonResponseMessage(string text, ApiCommonResponseMessageType type)
        {
            Text = text;
            Type = type;
        }

        public string Text { get; }
        public ApiCommonResponseMessageType Type { get; }
    }
}