using Ardalis.Specification.EntityFrameworkCore;
using ProjectManagement.CompanyAPI.Domain.Specifications;

namespace ProjectManagement.Company.Api.UnitTests.Domain.Specifications;

[ExcludeFromCodeCoverage]
public class CompanyByIdWithTagsSpecTests : SpecificationTests
{
    [Fact]
    public void CompanyByIdWithTagsSpec_WhenUsed_ReturnsCorrectList()
    {
        IQueryable<CompanyAPI.Domain.Entities.Company>? companies = GetCompanies(3, 1);
        CompanyByIdWithTagsSpec? sut = new (2);

        SpecificationEvaluator evaluator = new ();
        CompanyAPI.Domain.Entities.Company? result = evaluator.GetQuery(companies, sut).ToList().First();

        Assert.Equal(2, result.Id);
    }
}