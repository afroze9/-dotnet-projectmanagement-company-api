using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.DTO;
using ProjectManagement.CompanyAPI.Model;

namespace ProjectManagement.CompanyAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    private readonly IMapper _mapper;
    private readonly IValidator<CompanyRequestModel> _validator;

    private readonly ILogger<CompanyController> _logger;

    public CompanyController(ICompanyService companyService, IMapper mapper, IValidator<CompanyRequestModel> validator, ILogger<CompanyController> logger)
    {
        _companyService = companyService;
        _mapper = mapper;
        _validator = validator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<CompanySummaryResponseModel>>> GetAll()
    {
        List<CompanyDTO> companies = await _companyService.GetAllAsync();

        if (companies.Count == 0)
        {
            return NotFound();
        }

        List<CompanySummaryResponseModel> mappedCompanies = _mapper.Map<List<CompanySummaryResponseModel>>(companies);
        return Ok(mappedCompanies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyResponseModel>> GetById(int id)
    {
        CompanyDTO? company = await _companyService.GetByIdAsync(id);

        if (company == null)
        {
            return NotFound();
        }

        return Ok(company);
    }

    [HttpPost]
    public async Task<ActionResult<CompanyResponseModel>> Create([FromBody] CompanyRequestModel model)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        CompanyDTO company = _mapper.Map<CompanyDTO>(model);

        CompanyDTO createdCompany = await _companyService.CreateAsync(company);

        CompanyResponseModel response = _mapper.Map<CompanyResponseModel>(createdCompany);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
}