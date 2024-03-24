using TaskManagement.Contracts.Common;

namespace TaskManagement.Contracts.Subscriptions;

public record CreateSubscriptionRequest(
    string FirstName,
    string LastName,
    string Email,
    SubscriptionType SubscriptionType);