using Ardalis.Specification.EntityFrameworkCore;
using ProjectManagement.CompanyAPI.Domain.Specifications;

namespace ProjectManagement.Company.Api.UnitTests.Domain.Specifications;

[ExcludeFromCodeCoverage]
public class AllCompaniesByTagNameSpecTests : SpecificationTests
{
    [Fact]
    public void AllCompaniesByTagNameSpec_WhenUsed_ReturnsCorrectList()
    {
        IQueryable<CompanyAPI.Domain.Entities.Company> companies = GetCompanies(3, 3);
        AllCompaniesByTagNameSpec sut = new ("tag 1");

        SpecificationEvaluator evaluator = new ();
        List<CompanyAPI.Domain.Entities.Company> result = evaluator.GetQuery(companies, sut).ToList();

        Assert.Equal(3, result.Count);
    }
}