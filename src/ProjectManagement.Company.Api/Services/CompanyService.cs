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
        List<Company> companies = await _companyRepository.ListAsync(new CompanyWithTagsSpec());
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
        Company? company = await _companyRepository.GetByIdAsync(id);
        return _mapper.Map<CompanyDTO?>(company);
    }
}