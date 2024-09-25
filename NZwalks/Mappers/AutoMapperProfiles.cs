using AutoMapper;
using NZwalks.Models.Domain;
using NZwalks.Models.DTOs;

namespace NZwalks.Mappers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() {
            CreateMap<CreateRegionDto, Region>()
                .ForMember(dept => dept.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dept => dept.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dept => dept.RegionImageUrl, opt => opt.MapFrom(src => src.RegionImageUrl))
                .ReverseMap();
            CreateMap<RegionDto, Region>().ReverseMap();
        }
    }
}
