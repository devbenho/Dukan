using Domain.Shared;
using MediatR;

namespace Application.Common.Interfaces;

public interface ICommand : IRequest<Result>
{
    
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
    
}
