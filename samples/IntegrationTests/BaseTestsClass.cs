using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BuildingBlocks;
using BuildingBlocks.Api.Authentication;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    public abstract class BaseTestsClass<TStartup> where TStartup : class
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

            var configuration = Factory.Services.GetRequiredService<IConfiguration>();

            var apiKey = configuration["integrationTestApiKey"];
            if (string.IsNullOrEmpty(apiKey) == false)
            {
                Client.DefaultRequestHeaders.Add(TestHeaders.ApiKey, apiKey);
            }
        }

        protected async Task<TResponse?> GetAsync<TResponse>(string url, IPersonas forUser) where TResponse : class
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
        protected async Task<TResponse?> GetAsync<TResponse>(string url, int forUserId, int churchId) where TResponse : class
        {
            return await DoGetAsync<TResponse>(url, () =>
            {
                // Client.DefaultRequestHeaders.Add(TestHeaders.UserId, forUserId.ToString());
                // Client.DefaultRequestHeaders.Add(TestHeaders.ChurchId, churchId.ToString());
            });
        }

        private async Task<TResponse?> DoGetAsync<TResponse>(string url, Action callback) where TResponse : class
        {
            // Client.DefaultRequestHeaders.Remove(TestHeaders.UserId);
            // Client.DefaultRequestHeaders.Remove(TestHeaders.SpouseId);
            // Client.DefaultRequestHeaders.Remove(TestHeaders.ChurchId);
            
            callback();
            
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }
        
        public TQuery GetQuery<TQuery>() where TQuery : notnull
        {
            return Factory.ServiceProvier.GetRequiredService<TQuery>();
        }
        
        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var mediator = Factory.ServiceProvier.GetRequiredService<IMediator>();
            return await mediator.Send(request);
        }
        
        
        private async Task GeneratePersonas()
        {
            Personas.Generate();
            
            var memberQuery =  $@"BEGIN
               IF NOT EXISTS (SELECT * FROM Users 
                               WHERE UserId = @userId)
               BEGIN
                  INSERT INTO Users (UserId, SpouseId, FirstName, LastName, SourceOrganizationId, DisplayName, BirthDate, Gender)
                  VALUES(@UserId, @{nameof(Persona.SpouseId)}, @{nameof(Persona.FirstName)}, 
                    @{nameof(Persona.LastName)}, @{nameof(Persona.SourceOrganizationId)}, @{nameof(Persona.DisplayName)}, @{nameof(Persona.BirthDate)}, @{nameof(Persona.Gender)})
               END
            END";
            
            foreach (var persona in Personas.GetAllPersonas())
            {
                await Factory.SqlConnection.ExecuteAsync(memberQuery, new 
                {
                    UserId = persona.Id,
                    FirstName = persona.FirstName,
                    LastName = persona.LastName,
                    SourceOrganizationId = persona.SourceOrganizationId,
                    SpouseId = persona.SpouseId,
                    DisplayName = $"{persona.FirstName} {persona.LastName}",
                    BirthDate = persona.BirthDate,
                    Gender = persona.Gender
                });
            }
        }
    }
}