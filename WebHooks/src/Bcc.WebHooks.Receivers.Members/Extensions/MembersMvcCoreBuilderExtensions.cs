using System;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Bcc.WebHooks.Receivers.Members.Extensions
{
    /// <summary>
    /// Extension methods for setting up Members WebHooks in an <see cref="IMvcCoreBuilder" />.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class MembersMvcCoreBuilderExtensions
    {
        /// <summary>
        /// <para>
        /// Add Members WebHook configuration and services to the specified <paramref name="builder"/>. See
        /// <see href="https://developer.github.com/webhooks/"/> for additional details about Members WebHook requests.
        /// </para>
        /// <para>
        /// The '<c>WebHooks:Members:SecretKey:default</c>' configuration value contains the secret key for Members
        /// WebHook URIs of the form '<c>https://{host}/api/webhooks/incoming/github</c>'.
        /// '<c>WebHooks:Members:SecretKey:{id}</c>' configuration values contain secret keys for Members WebHook URIs of
        /// the form '<c>https://{host}/api/webhooks/incoming/github/{id}</c>'.
        /// </para>
        /// </summary>
        /// <param name="builder">The <see cref="IMvcCoreBuilder" /> to configure.</param>
        /// <returns>The <paramref name="builder"/>.</returns>
        public static IMvcCoreBuilder AddMembersWebHooks(this IMvcCoreBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            MembersServiceCollectionSetup.AddMembersServices(builder.Services);

            return builder
                .AddWebHooks();
        }
    }
}
