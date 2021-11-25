using Bcc.WebHooks.Receivers.Members.Filters;
using Bcc.WebHooks.Receivers.Metadata;

namespace Bcc.WebHooks.Receivers.Members.Metadata
{
    /// <summary>
    /// An <see cref="IWebHookMetadata"/> service containing metadata about the GitHub receiver.
    /// </summary>
    public class MembersMetadata :
        WebHookMetadata,
        IWebHookEventMetadata,
        IWebHookFilterMetadata,
        IWebHookPingRequestMetadata
    {
        private readonly MembersVerifySignatureFilter _verifySignatureFilter;

        /// <summary>
        /// Instantiates a new <see cref="MembersMetadata"/> instance.
        /// </summary>
        /// <param name="verifySignatureFilter">The <see cref="GitHubVerifySignatureFilter"/>.</param>
        public MembersMetadata(MembersVerifySignatureFilter verifySignatureFilter)
            : base(MembersConstants.ReceiverName)
        {
            _verifySignatureFilter = verifySignatureFilter;
        }

        // IWebHookBodyTypeMetadataService...

        /// <inheritdoc />
        public override WebHookBodyType BodyType => WebHookBodyType.Json;

        // IWebHookEventMetadata...

        /// <inheritdoc />
        public string ConstantValue => null;

        /// <inheritdoc />
        public string HeaderName => MembersConstants.EventHeaderName;

        /// <inheritdoc />
        public string QueryParameterName => null;

        // IWebHookPingRequestMetadata...

        /// <inheritdoc />
        public string PingEventName => MembersConstants.PingEventName;

        // IWebHookFilterMetadata...

        /// <inheritdoc />
        public void AddFilters(WebHookFilterMetadataContext context)
        {
            context.Results.Add(_verifySignatureFilter);
        }
    }
}
