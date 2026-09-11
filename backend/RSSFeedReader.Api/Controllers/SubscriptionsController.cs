using Microsoft.AspNetCore.Mvc;
using RSSFeedReader.Api.Models;
using RSSFeedReader.Api.Services;

namespace RSSFeedReader.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class SubscriptionsController(SubscriptionService subscriptionService, ILogger<SubscriptionsController> logger) : ControllerBase
{
    [HttpGet]
    public ActionResult<SubscriptionListResponse> Get()
    {
        return Ok(new SubscriptionListResponse(subscriptionService.GetAll()));
    }

    [HttpPost]
    public ActionResult<SubscriptionResponse> Post(SubscriptionRequest request)
    {
        var subscription = subscriptionService.Add(request.Url);
        if (subscription is null)
        {
            logger.LogInformation("Rejected empty subscription URL");
            return BadRequest(new ProblemDetails { Title = "Subscription URL is required" });
        }

        logger.LogInformation("Added subscription URL");
        return CreatedAtAction(nameof(Get), subscription);
    }
}