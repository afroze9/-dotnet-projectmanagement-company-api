using System.Net.Mime;
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

    private readonly ILogger<CompanyController> _logger;

    private readonly IMapper _mapper;
    private readonly IValidator<CompanyRequestModel> _validator;

    public CompanyController(ICompanyService companyService, IMapper mapper, IValidator<CompanyRequestModel> validator,
        ILogger<CompanyController> logger)
    {
        _companyService = companyService;
        _mapper = mapper;
        _validator = validator;
        _logger = logger;
    }

    /// <summary>
    ///     Gets list of companies.
    /// </summary>
    /// <returns>List of companies.</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CompanySummaryResponseModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
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

    /// <summary>
    ///     Gets a company by id.
    /// </summary>
    /// <param name="id">Company id.</param>
    /// <returns>Company by the given id.</returns>
    [HttpGet("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanySummaryResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    public async Task<ActionResult<CompanyResponseModel>> GetById(int id)
    {
        CompanyDTO? company = await _companyService.GetByIdAsync(id);

        if (company == null)
        {
            return NotFound();
        }

        return Ok(company);
    }

    /// <summary>
    ///     Creates a new company.
    /// </summary>
    /// <param name="model">Company to create.</param>
    /// <returns>Created company.</returns>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CompanyResponseModel))]
    public async Task<ActionResult<CompanyResponseModel>> Create([FromBody] CompanyRequestModel model)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
        }

        CompanyDTO company = _mapper.Map<CompanyDTO>(model);

        CompanyDTO createdCompany = await _companyService.CreateAsync(company);

        CompanyResponseModel response = _mapper.Map<CompanyResponseModel>(createdCompany);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
}