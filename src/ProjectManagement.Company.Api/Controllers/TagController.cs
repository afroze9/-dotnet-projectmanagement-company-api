using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.Contracts;
using ProjectManagement.CompanyAPI.DTO;
using ProjectManagement.CompanyAPI.Model;

namespace ProjectManagement.CompanyAPI.Controllers;

/// <summary>
///     Contains methods for managing tags.
/// </summary>
[ApiController]
[Route("api/v1")]
public class TagController : ControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly ILogger<CompanyController> _logger;
    private readonly IMapper _mapper;
    private readonly IMessagePublisher _messagePublisher;
    private readonly IValidator<TagRequestModel> _tagRequestModelValidator;
    private readonly ITagService _tagService;

    public TagController(IMapper mapper, ILogger<CompanyController> logger, ICompanyService companyService,
        ITagService tagService, IValidator<TagRequestModel> tagRequestModelValidator,
        IMessagePublisher messagePublisher)
    {
        _mapper = mapper;
        _logger = logger;
        _companyService = companyService;
        _tagService = tagService;
        _tagRequestModelValidator = tagRequestModelValidator;
        _messagePublisher = messagePublisher;
    }

    /// <summary>
    ///     Gets a list of tags.
    /// </summary>
    /// <returns>List of tags.</returns>
    [HttpGet("[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TagResponseModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    public async Task<ActionResult<List<TagResponseModel>>> GetAll()
    {
        List<TagDto> tags = await _tagService.GetAllAsync();

        if (tags.Count == 0)
        {
            return NotFound();
        }

        List<TagResponseModel> response = _mapper.Map<List<TagResponseModel>>(tags);
        return Ok(response);
    }

    /// <summary>
    ///     Creates a tag.
    /// </summary>
    /// <param name="name">Tag name.</param>
    /// <returns>Created tag.</returns>
    [HttpPost("[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TagResponseModel))]
    public async Task<ActionResult<TagResponseModel>> Create([FromQuery] string name)
    {
        ValidationResult validationResult =
            await _tagRequestModelValidator.ValidateAsync(new TagRequestModel { Name = name });

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
        }

        TagDto createdTag = await _tagService.CreateAsync(name);

        TagResponseModel response = _mapper.Map<TagResponseModel>(createdTag);
        return Ok(response);
    }

    /// <summary>
    ///     Deletes a tag.
    /// </summary>
    /// <param name="name">Tag name.</param>
    [HttpDelete("[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete([FromQuery] string name)
    {
        bool result = await _tagService.DeleteAsync(name);

        if (!result)
        {
            return BadRequest("Tag could not be deleted");
        }

        return NoContent();
    }

    /// <summary>
    ///     Add tag to a company
    /// </summary>
    /// <param name="id">Company Id.</param>
    /// <param name="tagName">Tag name.</param>
    /// <returns>Updated company.</returns>
    [HttpPost("company/{id}/tag")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyResponseModel))]
    public async Task<ActionResult<CompanyResponseModel>> AddCompanyTag(int id, [FromQuery] string tagName)
    {
        ValidationResult validationResult =
            await _tagRequestModelValidator.ValidateAsync(new TagRequestModel { Name = tagName });

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
        }

        CompanySummaryDto? updatedCompany = await _companyService.AddTagAsync(id, tagName);

        if (updatedCompany == null)
        {
            return BadRequest($"Unable to find company with the id {id}");
        }

        CompanyTagAddedIntegrationEvent @event = new (id, tagName);

        try
        {
            await _messagePublisher.PublishAsync(@event);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Publishing integration event: {IntegrationEventId} from {AppName}",
                @event.Id, Constants.ApplicationName);

            throw;
        }

        CompanyResponseModel response = _mapper.Map<CompanyResponseModel>(updatedCompany);
        return Ok(response);
    }

    /// <summary>
    ///     Delete tag from a company.
    /// </summary>
    /// <param name="id">Company Id.</param>
    /// <param name="tagName">Tag name.</param>
    [HttpDelete("company/{id}/tag")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<CompanyResponseModel>> DeleteCompanyTag(int id, [FromQuery] string tagName)
    {
        CompanySummaryDto? updatedCompany = await _companyService.DeleteTagAsync(id, tagName);

        if (updatedCompany == null)
        {
            return BadRequest($"Unable to find company with the id {id}");
        }

        CompanyTagDeletedIntegrationEvent @event = new (id, tagName);

        try
        {
            await _messagePublisher.PublishAsync(@event);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Publishing integration event: {IntegrationEventId} from {AppName}",
                @event.Id, Constants.ApplicationName);

            throw;
        }

        return NoContent();
    }
}