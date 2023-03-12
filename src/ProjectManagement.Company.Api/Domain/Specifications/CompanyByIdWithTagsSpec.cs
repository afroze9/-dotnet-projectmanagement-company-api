using Ardalis.Specification;

namespace ProjectManagement.CompanyAPI.Domain.Specifications;

public class CompanyByIdWithTagsSpec : Specification<Company>, ISingleResultSpecification
{
    public CompanyByIdWithTagsSpec(int id)
    {
        Query
            .Where(x => x.Id == id)
            .Include(x => x.Tags);
    }
}