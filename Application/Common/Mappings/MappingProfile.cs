using Domain;
using AutoMapper;


namespace Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Game, GameListEntity>();
    }
}