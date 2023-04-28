using AutoMapper;
using ProjectManagement.CompanyAPI.DTO;
using ProjectManagement.CompanyAPI.Model;

namespace ProjectManagement.CompanyAPI.Services;

public class ProjectService : IProjectService
{
    private readonly HttpClient _client;
    private readonly IMapper _mapper;

    public ProjectService(HttpClient client, IMapper mapper)
    {
        _client = client;
        _mapper = mapper;
    }

    public async Task<List<ProjectSummaryDto>> GetProjectsByCompanyIdAsync(int companyId)
    {
        HttpResponseMessage resposne =
            await _client.GetAsync($"https://project-api/api/v1/Project?companyId={companyId}");

        if (resposne.IsSuccessStatusCode)
        {
            List<ProjectResponseModel>? projects =
                await resposne.Content.ReadFromJsonAsync<List<ProjectResponseModel>>();

            return _mapper.Map<List<ProjectSummaryDto>>(projects);
        }

        return new List<ProjectSummaryDto>();
    }
}

public interface IProjectService
{
    Task<List<ProjectSummaryDto>> GetProjectsByCompanyIdAsync(int companyId);

}