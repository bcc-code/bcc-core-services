using AutoMapper;
using Bcc.Activities.Api.Models;
using Bcc.Activities.Api.Models.Requests;
using Bcc.Activities.Api.Services;
using BuildingBlocks.Api.Authentication;
using BuildingBlocks.Api.Logging;
using BuildingBlocks.Api.OpenApi.CommonResponse;
using Microsoft.AspNetCore.Mvc;

namespace Bcc.Activities.Api.Controllers;

[Route("/")]
public class ActivitiesController : ApiControllerBase
{
    private readonly ILogger<ActivitiesController> _logger;
    private readonly ILogService _logService;
    private readonly IUser _user;
    private readonly IActivityService _activityService;
    private readonly IMapper _mapper;

    public ActivitiesController(ILogger<ActivitiesController> logger, ILogService logService, IUser user, IActivityService activityService, IMapper mapper)
    {
        _logger = logger;
        _logService = logService;
        _user = user;
        _activityService = activityService;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Get Activities
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Activity>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetActivities([FromQuery] GetActivitiesRequest request)
    {
        try
        {
            var activities = await _activityService.GetActivitiesForOrganization(_user.OrganizationId);
            
            var result = _mapper.Map<List<Activity>>(activities);

            var response = ApiCommonResponse.Create()
                .WithSuccess()
                .WithData(result)
                .Build();
            
            return CommonResponse(response);
        }
        catch (Exception e)
        {
            var failure = ApiCommonResponse.Create()
                .WithFailure("Failed to get Activities for team")
                .Build();
            _logService.TrackException(e);
            return CommonResponse(failure);
        }
    }
    
    /// <summary>
    /// Add activity
    /// </summary>
    /// <param name="activity"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddActivity(CreateActivity activity)
    {
        try
        {
            await _activityService.AddActivity(activity, new CancellationToken());
            
            var response = ApiCommonResponse.Create()
                .WithSuccess()
                .Build();
            
            return CommonResponse(response);
        }
        catch (Exception e)
        {
            var failure = ApiCommonResponse.Create()
                .WithFailure("Couldn't add an activity")
                .Build();
            _logService.TrackException(e);
            return CommonResponse(failure);
        }
    }
    /// <summary>
    /// Update activity
    /// </summary>
    /// <param name="activity"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateActivity(UpdateActivity activity)
    {
        try
        {
            await _activityService.UpdateActivity(activity, new CancellationToken());
            
            var response = ApiCommonResponse.Create()
                .WithSuccess()
                .Build();
            
            return CommonResponse(response);
        }
        catch (Exception e)
        {
            var failure = ApiCommonResponse.Create()
                .WithFailure("OperationFailed")
                .Build();
            _logService.TrackException(e);
            return CommonResponse(failure);
        }
    }
    /// <summary>
    /// Remove activity
    /// </summary>
    /// <param name="activityId"></param>
    /// <returns></returns>
    [HttpDelete("{activityId}")]
    public async Task<IActionResult> DeleteActivity(Guid activityId)
    {
        try
        {
            var result = await _activityService.DeleteActivity(activityId);

            if (result)
            {
                var response = ApiCommonResponse.Create()
                    .WithSuccess()
                    .Build();

                return CommonResponse(response);
            }
            else
            {
                var response = ApiCommonResponse.Create()
                    .WithFailure("Activity was not deleted")
                    .Build();

                return CommonResponse(response);
            }
        }
        catch (Exception e)
        {
            var failure = ApiCommonResponse.Create()
                .WithFailure("OperationFailed")
                .Build();
            _logService.TrackException(e);
            return CommonResponse(failure);
        }
    }
}