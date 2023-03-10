using AutoMapper;
using ProjectManagement.CompanyAPI.Domain;
using ProjectManagement.CompanyAPI.DTO;
using ProjectManagement.CompanyAPI.Model;

namespace ProjectManagement.CompanyAPI.Mapping;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyDTO>();
        CreateMap<CompanyDTO, Company>();

        CreateMap<CompanyRequestModel, CompanyDTO>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(x => new TagDTO { Name = x })));

        CreateMap<CompanyDTO, CompanyResponseModel>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(x => x.Name)));

        CreateMap<CompanyDTO, CompanySummaryResponseModel>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(x => x.Name)));

        CreateMap<CompanySummaryResponseModel, CompanyDTO>();

        CreateMap<CompanyDTO, CompanyResponseModel>();
        CreateMap<CompanyResponseModel, CompanyDTO>();

        CreateMap<Tag, TagDTO>();
        CreateMap<TagDTO, Tag>();
        CreateMap<TagDTO, TagResponseModel>();
    }
}