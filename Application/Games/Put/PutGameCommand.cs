using Domain;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Games.Put;

public record PutGameCommand(Guid Id , string Title ) : IRequest<Guid>;
