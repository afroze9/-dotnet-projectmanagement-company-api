using Ardalis.Specification.EntityFrameworkCore;
using ProjectManagement.CompanyAPI.Domain.Specifications;

namespace ProjectManagement.Company.Api.UnitTests.Domain.Specifications;

[ExcludeFromCodeCoverage]
public class CompanyByNameSpecTests : SpecificationTests
{
    [Fact]
    public void CompanyByNameSpec_WhenUsed_ReturnsCorrectList()
    {
        IQueryable<CompanyAPI.Domain.Entities.Company>? companies = GetCompanies(3, 1);
        CompanyByNameSpec? sut = new ("company 2");

        SpecificationEvaluator evaluator = new ();
        CompanyAPI.Domain.Entities.Company? result = evaluator.GetQuery(companies, sut).ToList().First();

        Assert.Equal("company 2", result.Name);
    }
}