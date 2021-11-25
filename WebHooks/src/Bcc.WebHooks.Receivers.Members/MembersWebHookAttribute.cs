using System;
using Bcc.WebHooks.Receivers.Filters;
using Bcc.WebHooks.Receivers.Members.Properties;
using Bcc.WebHooks.Receivers.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.WebHooks;

namespace Bcc.WebHooks.Receivers.Members
{
    /// <summary>
    /// <para>
    /// An <see cref="Attribute"/> indicating the associated action is a Members WebHook endpoint. Specifies the
    /// optional <see cref="EventName"/> and <see cref="WebHookAttribute.Id"/>. Also adds a
    /// <see cref="WebHookReceiverExistsFilter"/> and a <see cref="ModelStateInvalidFilter"/> (unless
    /// <see cref="ApiBehaviorOptions.SuppressModelStateInvalidFilter"/> is <see langword="true"/>) for the action.
    /// </para>
    /// <para>
    /// The signature of the action should be:
    /// <code>
    /// Task{IActionResult} ActionName(string id, string @event, TData data)
    /// </code>
    /// or include the subset of parameters required. <c>TData</c> must be compatible with expected requests e.g.
    /// <see cref="Newtonsoft.Json.Linq.JObject"/>.
    /// </para>
    /// <para>
    /// An example Members WebHook URI is '<c>https://{host}/api/webhooks/incoming/github/{id}</c>'. See
    /// <see href="https://developer.github.com/webhooks/"/> for additional details about Members WebHook requests.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the application enables CORS in general (see the <c>Microsoft.AspNetCore.Cors</c> package), apply
    /// <c>DisableCorsAttribute</c> to this action. If the application depends on the
    /// <c>Microsoft.AspNetCore.Mvc.ViewFeatures</c> package, apply <c>IgnoreAntiforgeryTokenAttribute</c> to this
    /// action.
    /// </para>
    /// <para>
    /// <see cref="MembersWebHookAttribute"/> should be used at most once per <see cref="WebHookAttribute.Id"/> and
    /// <see cref="EventName"/> in a WebHook application.
    /// </para>
    /// </remarks>
    public class MembersWebHookAttribute : WebHookAttribute, IWebHookEventSelectorMetadata
    {
        private string _eventName;

        /// <summary>
        /// Instantiates a new <see cref="MembersWebHookAttribute"/> instance indicating the associated action is a
        /// Members WebHook endpoint.
        /// </summary>
        public MembersWebHookAttribute()
            : base(MembersConstants.ReceiverName)
        {
        }

        /// <summary>
        /// Gets or sets the name of the event the associated controller action accepts.
        /// </summary>
        /// <value>Default value is <see langword="null"/>, indicating this action accepts all events.</value>
        public string EventName
        {
            get
            {
                return _eventName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(Resources.General_ArgumentCannotBeNullOrEmpty, nameof(value));
                }

                _eventName = value;
            }
        }
    }
}
