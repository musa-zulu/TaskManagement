using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Infrastructure.Services;
public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
