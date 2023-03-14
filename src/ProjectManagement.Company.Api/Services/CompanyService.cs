using AutoMapper;
using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.Domain;
using ProjectManagement.CompanyAPI.Domain.Specifications;
using ProjectManagement.CompanyAPI.DTO;

namespace ProjectManagement.CompanyAPI.Services;

public class CompanyService : ICompanyService
{
    private readonly IRepository<Company> _companyRepository;
    private readonly IMapper _mapper;
    private readonly IRepository<Tag> _tagRepository;

    public CompanyService(IRepository<Company> companyRepository, IRepository<Tag> tagRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task<List<CompanyDto>> GetAllAsync()
    {
        List<Company> companies = await _companyRepository.ListAsync(new AllCompaniesWithTagsSpec());
        List<CompanyDto>? mappedCompanies = _mapper.Map<List<CompanyDto>>(companies);
        return mappedCompanies;
    }

    public async Task<CompanyDto> CreateAsync(CompanyDto company)
    {
        Company companyToCreate = new (company.Name);
        List<Tag> tagsToAdd = new ();

        if (company.Tags.Count != 0)
        {
            foreach (string tagName in company.Tags.Select(t => t.Name))
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

        return _mapper.Map<CompanyDto>(createdCompany);
    }

    public async Task<CompanyDto?> GetByIdAsync(int id)
    {
        Company? company = await _companyRepository.FirstOrDefaultAsync(new CompanyByIdWithTagsSpec(id));
        return _mapper.Map<CompanyDto?>(company);
    }

    public async Task<CompanyDto?> UpdateNameAsync(int id, string name)
    {
        Company? companyToUpdate = await _companyRepository.GetByIdAsync(id);

        if (companyToUpdate == null)
        {
            return null;
        }

        companyToUpdate.UpdateName(name);
        await _companyRepository.SaveChangesAsync();

        CompanyDto dto = _mapper.Map<CompanyDto>(companyToUpdate);
        return dto;
    }

    public async Task<CompanyDto?> AddTagAsync(int id, string tagName)
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
        return _mapper.Map<CompanyDto>(companyToUpdate);
    }

    public async Task<CompanyDto?> DeleteTagAsync(int id, string tagName)
    {
        Company? companyToUpdate = await _companyRepository.FirstOrDefaultAsync(new CompanyByIdWithTagsSpec(id));

        if (companyToUpdate == null)
        {
            return null;
        }

        companyToUpdate.RemoveTag(tagName);

        await _companyRepository.SaveChangesAsync();
        CompanyDto dto = _mapper.Map<CompanyDto>(companyToUpdate);

        return dto;
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