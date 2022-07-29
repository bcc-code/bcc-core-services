using System.Text;
using System.Text.Json;
using Bcc.Activities.Api;
using Bcc.Activities.Api.Models;
using Bcc.Activities.Api.Services;
using BuildingBlocks;
using NUnit.Framework;

namespace Bcc.Activities.Tests.Activities;

public class ActivitiesContractTests : BaseTestsClass<Program>
{
    [OneTimeSetUp]
    public void SeedActivityService()
    {
        var activity = Factory.ServiceProviders.GetService(typeof(IActivityService)) as FakeActivityService;
        activity?.AddActivity(new CreateActivity
        {
            ActivityId = Guid.NewGuid(),
            StartDateTimeUtc = default,
            EndDateTimeUtc = default,
            Name = null,
            Reference = "0001",
            Description = null,
            Location = null,
            ResponsibleId = 0,
            NeededParticipants = 0,
            OwnerOrganization = 1
        }, new CancellationToken());
    }
    
    [Test]
    public async Task Get_Activities_Endpoint_Test()
    {
        var response = await GetAsync<List<Activity>>("/api/activities", Personas.SherlockHolmes);
        var activity = response?.First();

        Assert.NotNull(response);
        Assert.NotNull(activity?.GetType().GetProperty("Name"), "Name not found");
        Assert.NotNull(activity?.GetType().GetProperty("Location"), "Location not found");
        Assert.NotNull(activity?.GetType().GetProperty("ActivityId"), "ActivityId not found");
        Assert.NotNull(activity?.GetType().GetProperty("StartDateTimeUtc"), "StartDateTimeUtc not found");
        Assert.NotNull(activity?.GetType().GetProperty("EndDateTimeUtc"), "EndDateTimeUtc not found");
        Assert.NotNull(activity?.GetType().GetProperty("OwnerOrganization"), "OwnerOrganization not found");
        Assert.NotNull(activity?.GetType().GetProperty("Reference"), "Reference not found");
        Assert.NotNull(activity?.GetType().GetProperty("ResponsibleId"), "ResponsibleId not found");
        Assert.NotNull(activity?.GetType().GetProperty("ResponsibleName"), "ResponsibleName not found");
        Assert.NotNull(activity?.GetType().GetProperty("Description"), "Description not found");
        Assert.NotNull(activity?.GetType().GetProperty("CreatedById"), "CreatedById not found");
        Assert.NotNull(activity?.GetType().GetProperty("LastChangedTimeUtc"), "LastChangedTimeUtc not found");
        Assert.NotNull(activity?.GetType().GetProperty("NeededParticipants"), "NeededParticipants not found");
    }

    [Test]
    public async Task Add_Activity_Endpoint_Test()
    {
        var createActivity = new CreateActivity();
        var json = JsonSerializer.Serialize(createActivity);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await PostAsync("/api/activities", content, Personas.SherlockHolmes);
        
        Assert.NotNull(response);
    }

    [Test]
    public void Validate_Create_Activity()
    {
        var createActivity = new CreateActivity();
        
        Assert.NotNull(createActivity.GetType().GetProperty("ActivityId"), "ActivityId property not found");
        Assert.NotNull(createActivity.GetType().GetProperty("StartDateTimeUtc"), "StartDateTimeUtc property not found");
        Assert.NotNull(createActivity.GetType().GetProperty("EndDateTimeUtc"), "EndDateTimeUtc property not found");
        Assert.NotNull(createActivity.GetType().GetProperty("Name"), "Name property not found");
        Assert.NotNull(createActivity.GetType().GetProperty("Reference"), "Reference property not found");
        Assert.NotNull(createActivity.GetType().GetProperty("Description"), "Description property not found");
        Assert.NotNull(createActivity.GetType().GetProperty("Location"), "Location property not found");
        Assert.NotNull(createActivity.GetType().GetProperty("ResponsibleId"), "ResponsibleId property not found");
        Assert.NotNull(createActivity.GetType().GetProperty("NeededParticipants"), "NeededParticipants property not found");
        Assert.NotNull(createActivity.GetType().GetProperty("OwnerOrganization"), "OwnerOrganization property not found");
    }

    [Test]
    public async Task Update_Activity_Endpoint_Test()
    {
        var updateActivity = new UpdateActivity();
        var json = JsonSerializer.Serialize(updateActivity);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await PutAsync("/api/activities", content, Personas.SherlockHolmes);
        
        Assert.NotNull(response);
    }

    [Test]
    public void Validate_Update_Activity()
    {
        var updateActivity = new UpdateActivity();
        
        Assert.NotNull(updateActivity.GetType().GetProperty("ActivityId"), "ActivityId property not found");
        Assert.NotNull(updateActivity.GetType().GetProperty("StartDateTimeUtc"), "StartDateTimeUtc property not found");
        Assert.NotNull(updateActivity.GetType().GetProperty("EndDateTimeUtc"), "EndDateTimeUtc property not found");
        Assert.NotNull(updateActivity.GetType().GetProperty("Reference"), "Reference property not found");
        Assert.NotNull(updateActivity.GetType().GetProperty("Name"), "Name property not found");
        Assert.NotNull(updateActivity.GetType().GetProperty("Description"), "Description property not found");
        Assert.NotNull(updateActivity.GetType().GetProperty("Location"), "Location property not found");
        Assert.NotNull(updateActivity.GetType().GetProperty("ResponsibleId"), "ResponsibleId property not found");
    }
    
    [Test]
    public async Task Delete_Activity_Endpoint_Test()
    {
        var activityId = Guid.NewGuid();
        
        var activity = Factory.ServiceProviders.GetService(typeof(IActivityService)) as FakeActivityService;
        activity?.AddActivity(new CreateActivity
        {
            ActivityId = activityId,
            StartDateTimeUtc = default,
            EndDateTimeUtc = default,
            Name = null,
            Reference = "0001",
            Description = null,
            Location = null,
            ResponsibleId = 0,
            NeededParticipants = 0,
            OwnerOrganization = 1
        }, new CancellationToken());

        var response = await DeleteAsync($"/api/activities/{activityId}", Personas.SherlockHolmes);
        
        Assert.NotNull(response);
    }
}