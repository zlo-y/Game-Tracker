using MediatR;

namespace Application.Activites.Command;

public record StopActivityCommand(Guid Id , Guid UserId) : IRequest<Unit>;