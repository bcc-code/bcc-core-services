using System.Net;
using System.Threading.Tasks;
using BuildingBlocks.Tests;
using NUnit.Framework;
using WebApi;

namespace IntegrationTests
{
    public class Tests : BaseTestsClass<Startup>
    {

        [Test]
        public async Task Test1()
        {
            var response = await Client.GetAsync("/WeatherForecast");
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

    }
}