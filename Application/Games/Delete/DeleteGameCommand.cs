using Domain;
using MediatR;
using Application.Common.Interfaces;

namespace Application.Games.Delete;

public record DeleteGameCommand(Guid Id) : IRequest<Guid>;

