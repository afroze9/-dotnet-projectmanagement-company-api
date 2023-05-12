using AutoMapper;
using Moq;
using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.Domain.Entities;
using ProjectManagement.CompanyAPI.Domain.Specifications;
using ProjectManagement.CompanyAPI.DTO;
using ProjectManagement.CompanyAPI.Services;

namespace ProjectManagement.Company.Api.UnitTests.Services;

public class CompanyServiceTests
{
    private readonly CompanyService _companyService;
    private readonly Mock<IRepository<CompanyAPI.Domain.Entities.Company>> _mockCompanyRepository = new ();
    private readonly Mock<IMapper> _mockMapper = new ();
    private readonly Mock<IProjectService> _mockProjectService = new ();
    private readonly Mock<IRepository<Tag>> _mockTagRepository = new ();

    public CompanyServiceTests()
    {
        _companyService = new CompanyService(
            _mockCompanyRepository.Object,
            _mockTagRepository.Object,
            _mockMapper.Object,
            _mockProjectService.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsListOfCompanies()
    {
        // Arrange
        CompanyAPI.Domain.Entities.Company company1 = new ("Company 1");
        CompanyAPI.Domain.Entities.Company company2 = new ("Company 2");
        List<CompanyAPI.Domain.Entities.Company> companies = new ()
            { company1, company2 };

        CompanySummaryDto companySummaryDto1 = new () { Id = 1, Name = "Company 1" };
        CompanySummaryDto companySummaryDto2 = new () { Id = 2, Name = "Company 2" };
        List<CompanySummaryDto> mappedCompanies = new ()
            { companySummaryDto1, companySummaryDto2 };

        _mockCompanyRepository.Setup(repo =>
                repo.ListAsync(It.IsAny<AllCompaniesWithTagsSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(companies);

        _mockMapper.Setup(mapper => mapper.Map<List<CompanySummaryDto>>(companies))
            .Returns(mappedCompanies);

        _mockProjectService.Setup(service => service.GetProjectsByCompanyIdAsync(1))
            .ReturnsAsync(new List<ProjectSummaryDto>());

        _mockProjectService.Setup(service => service.GetProjectsByCompanyIdAsync(2))
            .ReturnsAsync(new List<ProjectSummaryDto>());

        // Act
        List<CompanySummaryDto> result = await _companyService.GetAllAsync();

        // Assert
        Assert.Equal(mappedCompanies, result);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedCompany()
    {
        // Arrange

        CompanySummaryDto companySummaryDto = new ()
        {
            Name = "ACME",
            Tags = new List<TagDto>
            {
                new () { Name = "tag1" },
                new () { Name = "tag2" },
            },
        };

        Tag dbTag1 = new ("tag1") { Id = 1 };
        Tag dbTag2 = new ("tag2") { Id = 2 };
        Tag tagToAdd = new ("newtag");

        _mockTagRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<TagByNameSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dbTag1);

        _mockTagRepository.Setup(x => x.AddAsync(It.IsAny<Tag>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => tagToAdd);

        CompanyAPI.Domain.Entities.Company companyToCreate = new ("ACME");

        _mockCompanyRepository.Setup(x =>
                x.AddAsync(It.IsAny<CompanyAPI.Domain.Entities.Company>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(companyToCreate);

        _mockMapper.Setup(x => x.Map<CompanySummaryDto>(It.IsAny<CompanyAPI.Domain.Entities.Company>()))
            .Returns(new CompanySummaryDto
            {
                Id = 1, Name = companyToCreate.Name,
                Tags = new List<TagDto>
                {
                    new () { Id = 1, Name = "tag1" },
                    new () { Id = 2, Name = "tag2" },
                },
            });

        // Act
        CompanySummaryDto result = await _companyService.CreateAsync(companySummaryDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("ACME", result.Name);
        Assert.Equal(2, result.Tags.Count);
        Assert.Equal(dbTag1.Id, result.Tags[0].Id);
        Assert.Equal(dbTag1.Name, result.Tags[0].Name);
        Assert.Equal(dbTag2.Id, result.Tags[1].Id);
        Assert.Equal(dbTag2.Name, result.Tags[1].Name);

        _mockTagRepository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<TagByNameSpec>(), It.IsAny<CancellationToken>()),
            Times.Exactly(2));

        _mockTagRepository.Verify(x => x.AddAsync(It.IsAny<Tag>(), It.IsAny<CancellationToken>()), Times.Never);
        _mockCompanyRepository.Verify(
            x => x.AddAsync(It.IsAny<CompanyAPI.Domain.Entities.Company>(), It.IsAny<CancellationToken>()), Times.Once);

        _mockMapper.Verify(x => x.Map<CompanySummaryDto>(It.IsAny<CompanyAPI.Domain.Entities.Company>()), Times.Once);
    }
}