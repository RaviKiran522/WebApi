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
            CreateMap<CreateWalkDto, Walk>()
                .ForMember(dept => dept.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dept => dept.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dept => dept.LengthInKm, opt => opt.MapFrom(src => src.LengthInKm))
                .ForMember(dept => dept.WalkImageUrl, opt => opt.MapFrom(src => src.WalkImageUrl))
                .ForMember(dept => dept.DifficultyId, opt => opt.MapFrom(src => src.DifficultyId))
                .ForMember(dept => dept.RegionId, opt => opt.MapFrom(src => src.RegionId))
                .ReverseMap();
            CreateMap<WalkDto, Walk>()
                .ForMember(dept => dept.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dept => dept.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dept => dept.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dept => dept.LengthInKm, opt => opt.MapFrom(src => src.LengthInKm))
                .ForMember(dept => dept.WalkImageUrl, opt => opt.MapFrom(src => src.WalkImageUrl))
                .ForMember(dept => dept.DifficultyId, opt => opt.MapFrom(src => src.DifficultyId))
                .ForMember(dept => dept.RegionId, opt => opt.MapFrom(src => src.RegionId))
                .ReverseMap();
        }
    }
}
