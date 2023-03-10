using AutoMapper;
using ProjectManagement.Company.Api.Abstractions;
using ProjectManagement.Company.Api.DTO;

namespace ProjectManagement.Company.Api.Services;

public class CompanyService : ICompanyService
{
    private readonly IRepository<Domain.Company> _repository;
    private readonly IMapper _mapper;

    public CompanyService(IRepository<Domain.Company> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<List<CompanyDTO>> GetAllAsync()
    {
        List<Domain.Company> companies = await _repository.ListAsync();
        List<CompanyDTO>? mappedCompanies = _mapper.Map<List<CompanyDTO>>(companies);
        return mappedCompanies;
    }
}