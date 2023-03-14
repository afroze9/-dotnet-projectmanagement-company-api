using Ardalis.Specification;
using ProjectManagement.CompanyAPI.Domain.Entities;

namespace ProjectManagement.CompanyAPI.Domain.Specifications;

public class AllCompaniesByTagNameSpec : Specification<Company>
{
    public AllCompaniesByTagNameSpec(string tagName)
    {
        Query
            .Include(x => x.Tags)
            .Where(x => x.Tags.Any(y => y.Name == tagName));
    }
}
