using System;
using Bcc.WebHooks.Receivers.Members.Filters;
using Bcc.WebHooks.Receivers.Members.Metadata;
using Microsoft.AspNetCore.WebHooks.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bcc.WebHooks.Receivers.Members.Extensions
{
    /// <summary>
    /// Methods to add services for the Members receiver.
    /// </summary>
    internal static class MembersServiceCollectionSetup
    {
        /// <summary>
        /// Add services for the Members receiver.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to update.</param>
        public static void AddMembersServices(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            WebHookMetadata.Register<MembersMetadata>(services);
            services.TryAddSingleton<MembersVerifySignatureFilter>();
        }
    }
}
