using Bcc.Registrations.Api;
using Bcc.Registrations.Client;
using Bcc.Registrations.Tests.Fakes;
using Bcc.Registrations.Tests.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Bcc.Registrations.Tests
{
    /// <summary>
    /// End-to-end tests which test both the http client and the server implementation
    /// </summary>
    public class ClientServerTests
    {
        private TestServer _server;
        private IConfiguration _configuration;

        [OneTimeSetUp]
        public void Initialize()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _configuration = ConfigHelper.GetIConfigurationRoot(TestContext.CurrentContext.TestDirectory);
        }

        [SetUp]
        public void Setup()
        {            
        }

        [Test]
        public async Task GetRegistrationsTest()
        {
            var options = _configuration.GetSection("OAuth").Get<ApiClientOptions>();
            options.ApiBasePath = _configuration.GetValue<string>("RegistrationsApi:BasePath");

            var client = new RegistrationsClient(new HttpClientFactory(_server.CreateClient()), options);

            await client.GetRegistration(Guid.NewGuid());

            Assert.Pass();
        }
    }
}