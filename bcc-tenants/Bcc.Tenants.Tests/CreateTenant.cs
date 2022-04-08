using System;
using System.Collections.Generic;
using Bcc.Tenants.Api.Queries;
using Bcc.Tenants.Contracts;
using BuildingBlocks.MongoDB;
using Moq;
using NUnit.Framework;

namespace Bcc.Tenants.Tests;

public class CreateTenant
{
    private ITenantsQueries _tenantsQueries = null!;
    
    [OneTimeSetUp]
    public void Setup()
    {
        _tenantsQueries = new TenantsQueries(Mock.Of<IMongoService>());
    }

    [Test]
    public void TenantKey_Not_Provided()
    {
        var tenant = new Tenant
        {
            Name = "Tenant name",
            Owners = new List<int>()
            {
                55
            }
        };

        var ex = Assert.ThrowsAsync<ArgumentNullException>(() => _tenantsQueries.CreateTenant(tenant));
        Assert.NotNull(ex);
        Assert.AreEqual("Value cannot be null. (Parameter 'TenantKey')", ex.Message);
    }

    [Test]
    public void TenantName_Not_Provided()
    {
        var tenant = new Tenant
        {
            TenantKey = "bcctenantname",
            Owners = new List<int>()
            {
                55
            }
        };
        
        var ex = Assert.ThrowsAsync<ArgumentNullException>(() => _tenantsQueries.CreateTenant(tenant));
        Assert.NotNull(ex);
        Assert.AreEqual("Value cannot be null. (Parameter 'Name')", ex.Message);
    }

    [Test]
    public void Owners_Not_Provided()
    {
        var tenant = new Tenant
        {
            Name = "Tenant name",
            TenantKey = "bcctenantname",
        };
        
        var ex = Assert.ThrowsAsync<ArgumentNullException>(() => _tenantsQueries.CreateTenant(tenant));
        Assert.NotNull(ex);
        Assert.AreEqual("Value cannot be null. (Parameter 'Owners')", ex.Message);
    }
    
    [Test]
    public void Owner_Cannot_Be_0()
    {   
        var tenant = new Tenant
        {
            Name = "Tenant name",
            TenantKey = "bcctenantname",
            Owners = new List<int>()
            {
                0
            }
        };

        var ex = Assert.ThrowsAsync<ArgumentException>(() => _tenantsQueries.CreateTenant(tenant));
        Assert.NotNull(ex);
        Assert.AreEqual("Invalid Owner (OrgId)", ex.Message);
    }
}