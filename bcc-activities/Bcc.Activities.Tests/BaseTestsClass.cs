using System.Net.Http.Json;
using BuildingBlocks;
using BuildingBlocks.Api.Authentication;
using BuildingBlocks.Api.OpenApi.CommonResponse;
using NUnit.Framework;

namespace Bcc.Activities.Tests;

[TestFixture]
public abstract class BaseTestsClass<TStartup> where TStartup: class
{
    protected CustomWebApplicationFactory<TStartup> Factory = null!;
    protected HttpClient Client = null!;

    [OneTimeTearDown]
    public void CleanDatabase()
    {
        Factory.Dispose();
    }
    [OneTimeSetUp]
    public async Task InitEnv()
    {
        Factory = new CustomWebApplicationFactory<TStartup>();
        Client = Factory.CreateClient();
            
        await Factory.InitSeed();
    }

    protected async Task<TResponse> GetAsync<TResponse>(string url, IPersonas forUser) where TResponse : class
    {
        return await DoGetAsync<TResponse>(url, () =>
        {
            Client.DefaultRequestHeaders.Add(TestHeaders.UserId, forUser.Id.ToString());
            if (forUser.SpouseId.HasValue)
            {
                Client.DefaultRequestHeaders.Add(TestHeaders.SpouseId, forUser.SpouseId.ToString());
            }
            Client.DefaultRequestHeaders.Add(TestHeaders.ChurchId, forUser.SourceOrganizationId.ToString());
        });
    }
        
    protected async Task<HttpResponseMessage> PostAsync(string url, StringContent stringContent, IPersonas forUser) 
    {
        return await DoPostAsync(url, stringContent, () =>
        {
            Client.DefaultRequestHeaders.Add(TestHeaders.UserId, forUser.Id.ToString());
            Client.DefaultRequestHeaders.Add(TestHeaders.ChurchId, forUser.SourceOrganizationId.ToString());
        });
    }
    
    protected async Task<HttpResponseMessage> PutAsync(string url, StringContent stringContent, IPersonas forUser) 
    {
        return await DoPutAsync(url, stringContent, () =>
        {
            Client.DefaultRequestHeaders.Add(TestHeaders.UserId, forUser.Id.ToString());
            Client.DefaultRequestHeaders.Add(TestHeaders.ChurchId, forUser.SourceOrganizationId.ToString());
        });
    }
    
    protected async Task<HttpResponseMessage> DeleteAsync(string url, IPersonas forUser) 
    {
        return await DoDeleteAsync(url, () =>
        {
            Client.DefaultRequestHeaders.Add(TestHeaders.UserId, forUser.Id.ToString());
            Client.DefaultRequestHeaders.Add(TestHeaders.ChurchId, forUser.SourceOrganizationId.ToString());
        });
    }

    private async Task<TResponse> DoGetAsync<TResponse>(string url, Action callback) where TResponse : class
    {
        Client.DefaultRequestHeaders.Remove(TestHeaders.UserId);
        Client.DefaultRequestHeaders.Remove(TestHeaders.SpouseId);
        Client.DefaultRequestHeaders.Remove(TestHeaders.ChurchId);
            
        callback();
            
        var response = await Client.GetAsync(url);
        if (response.IsSuccessStatusCode == false)
        {
            throw new Exception(response.StatusCode.ToString());
        }
        return (await response.Content.ReadFromJsonAsync<ApiCommonResponse<TResponse>>())?.Data;
    }
        
    private async Task<HttpResponseMessage> DoPostAsync(string url, StringContent stringContent, Action callback)
    {
        Client.DefaultRequestHeaders.Remove(TestHeaders.ChurchId);
        Client.DefaultRequestHeaders.Remove(TestHeaders.UserId);
        Client.DefaultRequestHeaders.Remove(TestHeaders.SpouseId);
            
        callback();
            
        var response = await Client.PostAsync(url, stringContent);
        if (response.IsSuccessStatusCode == false)
        {
            throw new Exception(response.StatusCode.ToString());
        }
        return response;
    }
    
    private async Task<HttpResponseMessage> DoPutAsync(string url, StringContent stringContent, Action callback)
    {
        Client.DefaultRequestHeaders.Remove(TestHeaders.ChurchId);
        Client.DefaultRequestHeaders.Remove(TestHeaders.UserId);
        Client.DefaultRequestHeaders.Remove(TestHeaders.SpouseId);
            
        callback();
            
        var response = await Client.PutAsync(url, stringContent);
        if (response.IsSuccessStatusCode == false)
        {
            throw new Exception(response.StatusCode.ToString());
        }
        return response;
    }
    
    private async Task<HttpResponseMessage> DoDeleteAsync(string url, Action callback)
    {
        Client.DefaultRequestHeaders.Remove(TestHeaders.ChurchId);
        Client.DefaultRequestHeaders.Remove(TestHeaders.UserId);
        Client.DefaultRequestHeaders.Remove(TestHeaders.SpouseId);
            
        callback();
            
        var response = await Client.DeleteAsync(url);
        if (response.IsSuccessStatusCode == false)
        {
            throw new Exception(response.StatusCode.ToString());
        }
        return response;
    }
}