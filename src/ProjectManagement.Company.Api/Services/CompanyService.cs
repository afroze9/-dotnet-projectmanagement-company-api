using AutoMapper;
using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.Domain;
using ProjectManagement.CompanyAPI.DTO;

namespace ProjectManagement.CompanyAPI.Services;

public class CompanyService : ICompanyService
{
    private readonly IRepository<Company> _repository;
    private readonly IMapper _mapper;

    public CompanyService(IRepository<Company> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<List<CompanyDTO>> GetAllAsync()
    {
        List<Company> companies = await _repository.ListAsync();
        List<CompanyDTO>? mappedCompanies = _mapper.Map<List<CompanyDTO>>(companies);
        return mappedCompanies;
    }

    public async Task<CompanyDTO> CreateAsync(CompanyDTO company)
    {
        Company companyToCreate = new (company.Name);
        Company createdCompany = await _repository.AddAsync(companyToCreate);
        
        return _mapper.Map<CompanyDTO>(createdCompany);
    }

    public async Task<CompanyDTO?> GetByIdAsync(int id)
    {
        Company? company = await _repository.GetByIdAsync(id);
        return _mapper.Map<CompanyDTO?>(company);
    }
}