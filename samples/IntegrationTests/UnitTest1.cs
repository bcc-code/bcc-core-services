using BuildingBlocks.Tests;
using NUnit.Framework;
using WebApi;

namespace IntegrationTests
{
    public class Tests : BaseTestsClass<Startup>
    {

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

    }
}