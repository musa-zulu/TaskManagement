using MediatR;

namespace TaskManagement.Application.Common.Security.Requests;
public interface IAuthorizeableRequest<T> : IRequest<T>
{
    Guid UserId { get; }
}