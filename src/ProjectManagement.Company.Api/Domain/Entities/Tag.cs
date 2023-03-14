using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.Common;

namespace ProjectManagement.CompanyAPI.Domain.Entities;

public class Tag : EntityBase, IAggregateRoot, IAuditable<int>
{
    public Tag(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public virtual List<Company> Companies { get; set; } = new ();

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime ModifiedOn { get; set; }
}