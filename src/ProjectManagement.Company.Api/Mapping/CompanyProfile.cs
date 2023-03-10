using AutoMapper;
using ProjectManagement.Company.Api.DTO;
using ProjectManagement.Company.Api.Model;

namespace ProjectManagement.Company.Api.Mapping;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Domain.Company, CompanyDTO>();
        CreateMap<CompanyDTO, Domain.Company>();
        
        CreateMap<CompanyDTO, CompanySummaryResponseModel>();
        CreateMap<CompanySummaryResponseModel, CompanyDTO>();
        
        CreateMap<CompanyDTO, CompanyResponseModel>();
        CreateMap<CompanyResponseModel, CompanyDTO>();
    }
}