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

    public async Task<List<CompanyDTO>> GetAllAsync()
    {
        List<Company> companies = await _companyRepository.ListAsync(new AllCompaniesWithTagsSpec());
        List<CompanyDTO>? mappedCompanies = _mapper.Map<List<CompanyDTO>>(companies);
        return mappedCompanies;
    }

    public async Task<CompanyDTO> CreateAsync(CompanyDTO company)
    {
        Company companyToCreate = new (company.Name);
        List<Tag> tagsToAdd = new ();

        if (company.Tags.Count != 0)
        {
            foreach (TagDTO tagDTO in company.Tags)
            {
                Tag? dbTag = await _tagRepository.FirstOrDefaultAsync(new TagByNameSpec(tagDTO.Name));

                if (dbTag != null)
                {
                    tagsToAdd.Add(dbTag);
                }
                else
                {
                    Tag addedTag = await _tagRepository.AddAsync(new Tag(tagDTO.Name));
                    tagsToAdd.Add(addedTag);
                }
            }
        }

        companyToCreate.AddTags(tagsToAdd);
        Company createdCompany = await _companyRepository.AddAsync(companyToCreate);

        return _mapper.Map<CompanyDTO>(createdCompany);
    }

    public async Task<CompanyDTO?> GetByIdAsync(int id)
    {
        Company? company = await _companyRepository.FirstOrDefaultAsync(new CompanyByIdWithTagsSpec(id));
        return _mapper.Map<CompanyDTO?>(company);
    }

    public async Task<CompanyDTO?> UpdateNameAsync(int id, string name)
    {
        Company? companyToUpdate = await _companyRepository.GetByIdAsync(id);

        if (companyToUpdate == null)
        {
            return null;
        }

        companyToUpdate.UpdateName(name);
        await _companyRepository.SaveChangesAsync();

        CompanyDTO dto = _mapper.Map<CompanyDTO>(companyToUpdate);
        return dto;
    }

    public async Task<CompanyDTO?> AddTagAsync(int id, string tagName)
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
        return _mapper.Map<CompanyDTO>(companyToUpdate);
    }

    public async Task<CompanyDTO?> DeleteTagAsync(int id, string tagName)
    {
        Company? companyToUpdate = await _companyRepository.FirstOrDefaultAsync(new CompanyByIdWithTagsSpec(id));

        if (companyToUpdate == null)
        {
            return null;
        }

        companyToUpdate.RemoveTag(tagName);

        await _companyRepository.SaveChangesAsync();
        CompanyDTO dto = _mapper.Map<CompanyDTO>(companyToUpdate);

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