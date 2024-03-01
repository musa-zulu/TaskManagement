using TaskManagement.Application.Common.Security.Permissions;
using TaskManagement.Application.Common.Security.Roles;

namespace TestCommon.TestConstants;
public static partial class Constants
{
    public static class User
    {
        public const string FirstName = "Musa";
        public const string LastName = "Zulu";
        public const string Email = "musa@gmail.com";
        public static readonly Guid Id = Guid.NewGuid();
        public static readonly List<string> Permissions =
        [
            Permission.Subscription.Create,
            Permission.Subscription.Delete,
            Permission.Subscription.Get,
        ];

        public static readonly List<string> Roles =
        [
            Role.Admin
        ];
    }
}