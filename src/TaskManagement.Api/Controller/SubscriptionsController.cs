using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Subscriptions.Commands.CreateSubscription;
using TaskManagement.Application.Subscriptions.Common;
using TaskManagement.Application.Subscriptions.Queries.GetSubscription;
using TaskManagement.Contracts.Subscriptions;
using DomainSubscriptionType = TaskManagement.Domain.Users.SubscriptionType;
using SubscriptionType = TaskManagement.Contracts.Common.SubscriptionType;

namespace TaskManagement.Api.Controller;

[Route("users/{userId:guid}/subscriptions")]
public class SubscriptionsController(IMediator _mediator) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetSubscription(Guid userId)
    {
        var query = new GetSubscriptionQuery(userId);

        var result = await _mediator.Send(query);

        return result.Match(
            user => Ok(ToDto(user)),
            Problem);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(Guid userId, CreateSubscriptionRequest request)
    {
        if (!DomainSubscriptionType.TryFromName(request.SubscriptionType.ToString(), out var subscriptionType))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid plan type");
        }

        var command = new CreateSubscriptionCommand(
            userId,
            request.FirstName,
            request.LastName,
            request.Email,
            subscriptionType);

        var result = await _mediator.Send(command);

        return result.Match(
            subscription => CreatedAtAction(
                actionName: nameof(GetSubscription),
                routeValues: new { UserId = userId },
                value: ToDto(subscription)),
            Problem);
    }

    private static SubscriptionType ToDto(DomainSubscriptionType subscriptionType) =>
        subscriptionType.Name switch
        {
            nameof(DomainSubscriptionType.Basic) => SubscriptionType.Basic,
            nameof(DomainSubscriptionType.Pro) => SubscriptionType.Pro,
            _ => throw new InvalidOperationException(),
        };

    private static SubscriptionResponse ToDto(SubscriptionResult subscriptionResult) =>
        new(
            subscriptionResult.Id,
            subscriptionResult.UserId,
            ToDto(subscriptionResult.SubscriptionType));
}
