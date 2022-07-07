using System.Linq;
using BuildingBlocks;
using NUnit.Framework;
using WebApi;

namespace IntegrationTests
{
    public class PersonasTests : BaseTestsClass<Startup>
    {
        [Test]
        public void Test_Generator_ForPersonas()
        {
            Assert.IsTrue(Personas.WilliamShakespeare.Id > 0);
            Assert.IsTrue(Personas.WilliamShakespeare.SpouseId > 0);
            Assert.IsTrue(Personas.AnneShakespeare.Id > 0);
            Assert.IsTrue(Personas.AnneShakespeare.SpouseId > 0);
            
            Assert.AreEqual(Personas.WilliamShakespeare.SpouseId, Personas.AnneShakespeare.Id);
            Assert.AreEqual(Personas.AnneShakespeare.SpouseId, Personas.WilliamShakespeare.Id);
            
        }

        [Test]
        public void Unique_Ids()
        {
            var ids = Personas.GetAllPersonas().Select(x => x.Id).ToList();
            
            Assert.IsTrue(ids.All(x => x > 0));
            Assert.AreEqual(ids.Count(), ids.Distinct().Count());
        }
        
    }
}