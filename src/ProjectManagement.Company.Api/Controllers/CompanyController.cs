using System.Net.Mime;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.DTO;
using ProjectManagement.CompanyAPI.Model;

namespace ProjectManagement.CompanyAPI.Controllers;

[ApiController]
[Route("api/v1")]
public class CompanyController : ControllerBase
{
    private readonly IValidator<CompanyRequestModel> _companyRequestModelvalidator;
    private readonly ICompanyService _companyService;
    private readonly IValidator<CompanyUpdateRequestModel> _companyUpdateRequestModelvalidator;
    private readonly ILogger<CompanyController> _logger;
    private readonly IMapper _mapper;

    public CompanyController(ICompanyService companyService, IMapper mapper,
        IValidator<CompanyRequestModel> companyRequestModelvalidator,
        ILogger<CompanyController> logger, IValidator<CompanyUpdateRequestModel> companyUpdateRequestModelvalidator)
    {
        _companyService = companyService;
        _mapper = mapper;
        _companyRequestModelvalidator = companyRequestModelvalidator;
        _logger = logger;
        _companyUpdateRequestModelvalidator = companyUpdateRequestModelvalidator;
    }

    /// <summary>
    ///     Gets list of companies.
    /// </summary>
    /// <returns>List of companies.</returns>
    [Authorize("read:company")]
    [HttpGet("[controller]")]
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
    [Authorize("read:company")]
    [HttpGet("[controller]/{id}")]
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
    [Authorize("write:company")]
    [HttpPost("[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CompanyResponseModel))]
    public async Task<ActionResult<CompanyResponseModel>> Create([FromBody] CompanyRequestModel model)
    {
        ValidationResult validationResult = await _companyRequestModelvalidator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
        }

        CompanyDTO company = _mapper.Map<CompanyDTO>(model);

        CompanyDTO createdCompany = await _companyService.CreateAsync(company);

        CompanyResponseModel response = _mapper.Map<CompanyResponseModel>(createdCompany);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    ///     Update company details.
    /// </summary>
    /// <param name="id">Id of the company to update.</param>
    /// <param name="model">Details to update.</param>
    /// <returns>Updated company.</returns>
    [Authorize("update:company")]
    [HttpPut("[controller]/{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyResponseModel))]
    public async Task<ActionResult<CompanyResponseModel>> Update(int id, [FromBody] CompanyUpdateRequestModel model)
    {
        ValidationResult validationResult = await _companyUpdateRequestModelvalidator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
        }

        CompanyDTO? updatedCompany = await _companyService.UpdateNameAsync(id, model.Name);

        if (updatedCompany == null)
        {
            return BadRequest($"Unable to find company with the id {id}");
        }

        CompanyResponseModel response = _mapper.Map<CompanyResponseModel>(updatedCompany);
        return Ok(response);
    }

    /// <summary>
    ///     Delete a company.
    /// </summary>
    /// <param name="id">Company Id.</param>
    [Authorize("delete:company")]
    [HttpDelete("[controller]/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await _companyService.DeleteAsync(id);
        return NoContent();
    }
}