using AutoMapper;
using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.DTO;
using ProjectManagement.CompanyAPI.Model;

namespace ProjectManagement.CompanyAPI.Services;

/// <summary>
///     Service for managing projects.
/// </summary>
public class ProjectService : IProjectService
{
    private readonly HttpClient _client;
    private readonly IMapper _mapper;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ProjectService" /> class.
    /// </summary>
    /// <param name="client">The HTTP client.</param>
    /// <param name="mapper">The mapper.</param>
    public ProjectService(HttpClient client, IMapper mapper)
    {
        _client = client;
        _mapper = mapper;
    }

    /// <summary>
    ///     Gets projects by company ID asynchronously.
    /// </summary>
    /// <param name="companyId">The ID of the company to get projects for.</param>
    /// <returns>A list of projects for the specified company.</returns>
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
