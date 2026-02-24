using MediatR;

namespace Application.Activities.Commands;

public record StopActivityCommand(Guid Id , Guid UserId) : IRequest<Unit>;