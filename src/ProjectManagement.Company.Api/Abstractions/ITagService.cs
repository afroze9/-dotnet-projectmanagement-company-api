using ProjectManagement.CompanyAPI.DTO;

namespace ProjectManagement.CompanyAPI.Abstractions;

/// <summary>
/// </summary>
public interface ITagService
{
    Task<TagDTO> CreateAsync(string name);

    Task<bool> DeleteAsync(string name);

    Task<List<TagDTO>> GetAllAsync();
}