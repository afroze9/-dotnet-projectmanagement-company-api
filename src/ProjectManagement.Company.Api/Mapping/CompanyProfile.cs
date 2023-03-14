using AutoMapper;
using ProjectManagement.CompanyAPI.Domain.Entities;
using ProjectManagement.CompanyAPI.DTO;
using ProjectManagement.CompanyAPI.Model;

namespace ProjectManagement.CompanyAPI.Mapping;

[ExcludeFromCodeCoverage]
public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyDto>();
        CreateMap<CompanyDto, Company>();

        CreateMap<CompanyRequestModel, CompanyDto>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(x => new TagDto { Name = x })));

        CreateMap<CompanyDto, CompanyResponseModel>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(x => x.Name)));

        CreateMap<CompanyDto, CompanySummaryResponseModel>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(x => x.Name)));

        CreateMap<CompanySummaryResponseModel, CompanyDto>();

        CreateMap<CompanyDto, CompanyResponseModel>();
        CreateMap<CompanyResponseModel, CompanyDto>();

        CreateMap<Tag, TagDto>();
        CreateMap<TagDto, Tag>();
        CreateMap<TagDto, TagResponseModel>();
    }
}