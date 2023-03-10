using ProjectManagement.Company.Api.DTO;

namespace ProjectManagement.Company.Api.Abstractions;

public interface ICompanyService
{
    Task<List<CompanyDTO>> GetAllAsync();
}