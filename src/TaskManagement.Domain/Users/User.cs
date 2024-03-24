using TaskManagement.Domain.Common;

namespace TaskManagement.Domain.Users;

public class User : Entity
{
    public string Email { get; } = null!;

    public string FirstName { get; } = null!;

    public string LastName { get; } = null!;

    public Subscription Subscription { get; set; } = null!;
}