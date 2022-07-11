using System.Net;
using System.Threading.Tasks;
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
        
        [Test]
        public async Task AuthenticationTest()
        {
            var response = await Client.GetAsync("/Authenticated");
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}