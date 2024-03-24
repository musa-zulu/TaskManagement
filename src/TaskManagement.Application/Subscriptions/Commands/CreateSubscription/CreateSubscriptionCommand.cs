using ErrorOr;
using TaskManagement.Application.Common.Security.Permissions;
using TaskManagement.Application.Common.Security.Policies;
using TaskManagement.Application.Common.Security.Request;
using TaskManagement.Application.Subscriptions.Common;
using TaskManagement.Domain.Users;

namespace TaskManagement.Application.Subscriptions.Commands.CreateSubscription;

[Authorize(Permissions = Permission.Subscription.Create, Policies = Policy.SelfOrAdmin)]
public record CreateSubscriptionCommand(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    SubscriptionType SubscriptionType)
    : IAuthorizeableRequest<ErrorOr<SubscriptionResult>>;