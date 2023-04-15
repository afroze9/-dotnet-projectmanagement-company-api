using ProjectManagement.CompanyAPI.DTO;

namespace ProjectManagement.CompanyAPI.Abstractions;

public interface ICompanyService
{
    Task<List<CompanySummaryDto>> GetAllAsync();

    Task<CompanySummaryDto> CreateAsync(CompanySummaryDto companySummary);

    Task<CompanyDto?> GetByIdAsync(int id);

    Task<CompanySummaryDto?> UpdateNameAsync(int id, string name);

    Task<CompanySummaryDto?> AddTagAsync(int id, string tagName);

    Task<CompanySummaryDto?> DeleteTagAsync(int id, string tagName);

    Task DeleteAsync(int id);
}