using ErrorOr;
using TaskManagement.Application.Common.Security.Permissions;
using TaskManagement.Application.Common.Security.Policies;
using TaskManagement.Application.Common.Security.Request;
using TaskManagement.Application.Subscriptions.Common;

namespace TaskManagement.Application.Subscriptions.Queries.GetSubscription;

[Authorize(Permissions = Permission.Subscription.Get, Policies = Policy.SelfOrAdmin)]
public record GetSubscriptionQuery(Guid UserId)
    : IAuthorizeableRequest<ErrorOr<SubscriptionResult>>;