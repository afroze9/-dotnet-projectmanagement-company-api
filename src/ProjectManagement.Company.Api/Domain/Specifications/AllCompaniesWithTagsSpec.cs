using Ardalis.Specification;
using ProjectManagement.CompanyAPI.Domain.Entities;

namespace ProjectManagement.CompanyAPI.Domain.Specifications;

public class AllCompaniesWithTagsSpec : Specification<Company>
{
    public AllCompaniesWithTagsSpec()
    {
        Query.Include(x => x.Tags);
    }
}