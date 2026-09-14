using AutoMapper;
using Digital_Domain_Layer.Entities;
using Digital_Infrastructure_Layer.DTOs;

namespace Digital_Persistence_Layer.MappingProfiles;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<MainCategoryDTO, MainCategory>().ReverseMap();
    }
}