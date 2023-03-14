using AutoMapper;
using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.Domain;
using ProjectManagement.CompanyAPI.Domain.Specifications;
using ProjectManagement.CompanyAPI.DTO;

namespace ProjectManagement.CompanyAPI.Services;

public class TagService : ITagService
{
    private readonly IRepository<Company> _companyRepository;
    private readonly IMapper _mapper;
    private readonly IRepository<Tag> _tagRepository;

    public TagService(IRepository<Tag> tagRepository, IMapper mapper, IRepository<Company> companyRepository)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
        _companyRepository = companyRepository;
    }

    public async Task<TagDto> CreateAsync(string name)
    {
        Tag tagToCreate = new (name);
        Tag createdTag = await _tagRepository.AddAsync(tagToCreate);
        return _mapper.Map<TagDto>(createdTag);
    }

    public async Task<bool> DeleteAsync(string name)
    {
        if (await _companyRepository.AnyAsync(new AllCompaniesByTagNameSpec(name)))
        {
            return false;
        }

        Tag? tagToDelete = await _tagRepository.FirstOrDefaultAsync(new TagByNameSpec(name));

        if (tagToDelete != null)
        {
            await _tagRepository.DeleteAsync(tagToDelete);
        }

        return true;
    }

    public async Task<List<TagDto>> GetAllAsync()
    {
        List<Tag> tags = await _tagRepository.ListAsync();
        return _mapper.Map<List<TagDto>>(tags);
    }
}