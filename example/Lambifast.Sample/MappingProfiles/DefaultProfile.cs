using AutoMapper;
using Lambifast.Sample.Dtos;
using Lambifast.Sample.Entities;

namespace Lambifast.Sample.MappingProfiles;

public class DefaultProfile : Profile
{
    public DefaultProfile()
    {
        CreateMap<BookDto, Book>()
            .ReverseMap();
    }
}