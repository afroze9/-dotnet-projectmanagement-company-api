using ProjectManagement.CompanyAPI.DTO;

namespace ProjectManagement.CompanyAPI.Abstractions;

public interface ICompanyService
{
    Task<List<CompanyDTO>> GetAllAsync();

    Task<CompanyDTO> CreateAsync(CompanyDTO company);

    Task<CompanyDTO?> GetByIdAsync(int id);

    Task<CompanyDTO?> UpdateNameAsync(int id, string name);

    Task<CompanyDTO?> AddTagAsync(int id, string tagName);

    Task<CompanyDTO?> DeleteTagAsync(int id, string tagName);

    Task DeleteAsync(int id);
}