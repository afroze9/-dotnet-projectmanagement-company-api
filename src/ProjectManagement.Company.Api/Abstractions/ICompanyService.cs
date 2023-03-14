using ProjectManagement.CompanyAPI.DTO;

namespace ProjectManagement.CompanyAPI.Abstractions;

public interface ICompanyService
{
    Task<List<CompanyDto>> GetAllAsync();

    Task<CompanyDto> CreateAsync(CompanyDto company);

    Task<CompanyDto?> GetByIdAsync(int id);

    Task<CompanyDto?> UpdateNameAsync(int id, string name);

    Task<CompanyDto?> AddTagAsync(int id, string tagName);

    Task<CompanyDto?> DeleteTagAsync(int id, string tagName);

    Task DeleteAsync(int id);
}