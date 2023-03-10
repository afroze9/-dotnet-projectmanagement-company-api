using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Company.Api.Abstractions;
using ProjectManagement.Company.Api.DTO;
using ProjectManagement.Company.Api.Model;

namespace ProjectManagement.Company.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    private readonly IMapper _mapper;

    private readonly ILogger<CompanyController> _logger;

    public CompanyController(ICompanyService companyService, IMapper mapper, ILogger<CompanyController> logger)
    {
        _companyService = companyService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<HttpResponseModel<List<CompanySummaryResponseModel>>>> GetAll()
    {
        List<CompanyDTO> companies = await _companyService.GetAllAsync();

        if (companies.Count == 0)
        {
            return NotFound();
        }

        List<CompanySummaryResponseModel> mappedCompanies = _mapper.Map<List<CompanySummaryResponseModel>>(companies);
        HttpResponseModel<List<CompanySummaryResponseModel>> response = HttpResponseModel<List<CompanySummaryResponseModel>>.Success(mappedCompanies);
        
        return Ok(response);
    } 
}