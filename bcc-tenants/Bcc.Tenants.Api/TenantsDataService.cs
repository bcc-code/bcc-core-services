using Bcc.Tenants.Contracts;
using Microsoft.Extensions.FileProviders;

namespace Bcc.Tenants.Api;

public class TenantsDataService
{
    private readonly IFileProvider _files;

    public TenantsDataService(IFileProvider files)
    {
        _files = files;
    }

    private static List<Tenant> _tenants = new();

    public List<Tenant> GetTenants()
    {
        if (_tenants.Any())
        {
            return _tenants;
        }
        
        var file = _files.GetFileInfo("/Data/tenants.csv");
        using (var fs = file.CreateReadStream())
        using (var sr = new StreamReader(fs))
        {
            var contents = sr.ReadToEnd();
            foreach (var fileLine in contents.Split('\n').Skip(1).ToList())
            {
                var fileLineParts = fileLine.Split(',');
                if (fileLineParts.Length > 1)
                {
                    var tenant = new Tenant
                    {
                        Name = fileLineParts[1],
                        TenantKey = fileLineParts[3],
                        Owners = new List<int> {int.Parse(fileLineParts[0])}
                    };

                    _tenants.Add(tenant);
                }
            }
        }
        return _tenants;
    }
}