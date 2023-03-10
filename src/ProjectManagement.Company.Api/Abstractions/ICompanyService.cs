using ProjectManagement.CompanyAPI.DTO;

namespace ProjectManagement.CompanyAPI.Abstractions;

public interface ICompanyService
{
    Task<List<CompanyDTO>> GetAllAsync();

    Task<CompanyDTO> CreateAsync(CompanyDTO company);

    Task<CompanyDTO?> GetByIdAsync(int id);
}