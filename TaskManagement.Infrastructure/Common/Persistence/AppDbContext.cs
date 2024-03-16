using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Users;

namespace TaskManagement.Infrastructure.Common.Persistence;

public class AppDbContext(DbContextOptions options, IHttpContextAccessor _httpContextAccessor, IPublisher _publisher) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}