using Ardalis.Specification;
using ProjectManagement.CompanyAPI.Domain.Entities;

namespace ProjectManagement.CompanyAPI.Domain.Specifications;

public class CompanyByNameSpec : Specification<Company>, ISingleResultSpecification
{
    public CompanyByNameSpec(string name)
    {
        Query.Where(x => x.Name == name);
    }
}