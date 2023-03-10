using Ardalis.Specification;

namespace ProjectManagement.CompanyAPI.Domain.Specifications;

public class TagByNameSpec : Specification<Tag>, ISingleResultSpecification
{
    public TagByNameSpec(string name)
    {
        Query.Where(x => x.Name == name);
    }
}