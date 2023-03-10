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
        
        CreateMap<CompanyRequestModel, CompanyDTO>();
        CreateMap<CompanyDTO, CompanyRequestModel>();
        
        CreateMap<CompanyDTO, CompanySummaryResponseModel>();
        CreateMap<CompanySummaryResponseModel, CompanyDTO>();
        
        CreateMap<CompanyDTO, CompanyResponseModel>();
        CreateMap<CompanyResponseModel, CompanyDTO>();
    }
}