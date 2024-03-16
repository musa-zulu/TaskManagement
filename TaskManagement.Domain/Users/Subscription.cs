using TaskManagement.Domain.Common;

namespace TaskManagement.Domain.Users;

public class Subscription : Entity
{
    public SubscriptionType SubscriptionType { get; set; } = null!;

    public bool Active { get; set; }
}