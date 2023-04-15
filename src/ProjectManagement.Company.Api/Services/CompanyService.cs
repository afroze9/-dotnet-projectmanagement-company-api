using AutoMapper;
using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.Domain.Entities;
using ProjectManagement.CompanyAPI.Domain.Specifications;
using ProjectManagement.CompanyAPI.DTO;

namespace ProjectManagement.CompanyAPI.Services;

public class CompanyService : ICompanyService
{
    private readonly IRepository<Company> _companyRepository;
    private readonly IMapper _mapper;
    private readonly IProjectService _projectService;
    private readonly IRepository<Tag> _tagRepository;

    public CompanyService(IRepository<Company> companyRepository, IRepository<Tag> tagRepository, IMapper mapper,
        IProjectService projectService)
    {
        _companyRepository = companyRepository;
        _tagRepository = tagRepository;
        _mapper = mapper;
        _projectService = projectService;
    }

    public async Task<List<CompanySummaryDto>> GetAllAsync()
    {
        List<Company> companies = await _companyRepository.ListAsync(new AllCompaniesWithTagsSpec());
        List<CompanySummaryDto>? mappedCompanies = _mapper.Map<List<CompanySummaryDto>>(companies);
        return mappedCompanies;
    }

    public async Task<CompanySummaryDto> CreateAsync(CompanySummaryDto companySummary)
    {
        Company companyToCreate = new (companySummary.Name);
        List<Tag> tagsToAdd = new ();

        if (companySummary.Tags.Count != 0)
        {
            foreach (string tagName in companySummary.Tags.Select(t => t.Name))
            {
                Tag? dbTag = await _tagRepository.FirstOrDefaultAsync(new TagByNameSpec(tagName));

                if (dbTag != null)
                {
                    tagsToAdd.Add(dbTag);
                }
                else
                {
                    Tag addedTag = await _tagRepository.AddAsync(new Tag(tagName));
                    tagsToAdd.Add(addedTag);
                }
            }
        }

        companyToCreate.AddTags(tagsToAdd);
        Company createdCompany = await _companyRepository.AddAsync(companyToCreate);

        return _mapper.Map<CompanySummaryDto>(createdCompany);
    }

    public async Task<CompanyDto?> GetByIdAsync(int id)
    {
        Company? company = await _companyRepository.FirstOrDefaultAsync(new CompanyByIdWithTagsSpec(id));

        if (company == null)
        {
            return null;
        }

        CompanyDto mappedCompanySummary = _mapper.Map<CompanyDto>(company);
        List<ProjectSummaryDto> projects = await _projectService.GetProjectsByCompanyIdAsync(id);
        mappedCompanySummary.Projects = projects;

        return mappedCompanySummary;
    }

    public async Task<CompanySummaryDto?> UpdateNameAsync(int id, string name)
    {
        Company? companyToUpdate = await _companyRepository.GetByIdAsync(id);

        if (companyToUpdate == null)
        {
            return null;
        }

        companyToUpdate.UpdateName(name);
        await _companyRepository.SaveChangesAsync();

        CompanySummaryDto summaryDto = _mapper.Map<CompanySummaryDto>(companyToUpdate);
        return summaryDto;
    }

    public async Task<CompanySummaryDto?> AddTagAsync(int id, string tagName)
    {
        Company? companyToUpdate = await _companyRepository.FirstOrDefaultAsync(new CompanyByIdWithTagsSpec(id));

        if (companyToUpdate == null)
        {
            return null;
        }

        Tag? dbTag = await _tagRepository.FirstOrDefaultAsync(new TagByNameSpec(tagName));

        if (dbTag != null)
        {
            companyToUpdate.AddTag(dbTag);
        }
        else
        {
            Tag addedTag = await _tagRepository.AddAsync(new Tag(tagName));
            companyToUpdate.AddTag(addedTag);
        }

        await _companyRepository.SaveChangesAsync();
        return _mapper.Map<CompanySummaryDto>(companyToUpdate);
    }

    public async Task<CompanySummaryDto?> DeleteTagAsync(int id, string tagName)
    {
        Company? companyToUpdate = await _companyRepository.FirstOrDefaultAsync(new CompanyByIdWithTagsSpec(id));

        if (companyToUpdate == null)
        {
            return null;
        }

        companyToUpdate.RemoveTag(tagName);

        await _companyRepository.SaveChangesAsync();
        CompanySummaryDto summaryDto = _mapper.Map<CompanySummaryDto>(companyToUpdate);

        return summaryDto;
    }

    public async Task DeleteAsync(int id)
    {
        Company? companyToDelete = await _companyRepository.FirstOrDefaultAsync(new CompanyByIdWithTagsSpec(id));

        if (companyToDelete == null)
        {
            return;
        }

        companyToDelete.RemoveTags();

        await _companyRepository.SaveChangesAsync();
        await _companyRepository.DeleteAsync(companyToDelete);
    }
}