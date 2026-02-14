using MediatR;

namespace Application.Activities.Commands;
public record CreateActivityCommand(string Name, Guid GameId, Guid UserId) : IRequest<Guid>;

