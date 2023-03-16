using ProjectManagement.CompanyAPI.Model;

namespace ProjectManagement.CompanyAPI.Services;

public class ProjectService : IProjectService
{
    private readonly HttpClient _client;

    public ProjectService(HttpClient client)
    {
        _client = client;
    }

    public async Task<int> GetProjectsByCompanyIdAsync(int companyId)
    {
        HttpResponseMessage resposne =
            await _client.GetAsync($"https://project-api/api/v1/Project?companyId={companyId}");

        if (resposne.IsSuccessStatusCode)
        {
            List<ProjectResponseModel>? projects =
                await resposne.Content.ReadFromJsonAsync<List<ProjectResponseModel>>();

            return projects?.Count ?? 0;
        }

        return 0;
    }
}

public interface IProjectService
{
    Task<int> GetProjectsByCompanyIdAsync(int companyId);
}