using Baseline;
using Microsoft.AspNetCore.Mvc;
using Prepps.Subscriptions;
using Prepps.Subscriptions.Commands;
using Prepps.Subscriptions.Queries;

namespace Prepps.Api.Controllers.Subscriptions;

[Route("[controller]")]
public class SubscriptionsController : ControllerBase 
{
    private readonly IGetSubscriptions _getSubscriptions;
    private readonly IStoreSubscription _storeSubscription;
    private readonly IDeleteSubscription _deleteSubscription;

    public SubscriptionsController(IGetSubscriptions getSubscriptions,
        IStoreSubscription storeSubscription,
        IDeleteSubscription deleteSubscription)
    {
        _getSubscriptions = getSubscriptions;
        _storeSubscription = storeSubscription;
        _deleteSubscription = deleteSubscription;
    }

    [HttpGet]
    public async Task<ActionResult> GetSubscriptions() => Ok(await _getSubscriptions.Execute());

    [HttpPost]
    public async Task<ActionResult> CreateSubscription([FromBody] string email)
    {
        if (email.IsEmpty()) return BadRequest("Email must not be empty");
        
        var subscription = new Subscription
        {
            Id = Guid.NewGuid().ToString(),
            Email = email,
        };

        await _storeSubscription.Execute(subscription);

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteSubscription([FromBody]Guid id)
    {
        await _deleteSubscription.Execute(id);
        return Ok();
    }
}