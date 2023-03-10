using Ardalis.Specification;

namespace ProjectManagement.CompanyAPI.Domain.Specifications;

public class CompanyWithTagsSpec : Specification<Company>
{
    public CompanyWithTagsSpec()
    {
        Query.Include(x => x.Tags);
    }
}