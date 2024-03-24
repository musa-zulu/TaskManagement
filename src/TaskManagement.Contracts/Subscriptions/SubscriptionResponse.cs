using TaskManagement.Contracts.Common;

namespace TaskManagement.Contracts.Subscriptions;

public record SubscriptionResponse(
    Guid Id,
    Guid UserId,
    SubscriptionType SubscriptionType);
