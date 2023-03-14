using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.Common;

namespace ProjectManagement.CompanyAPI.Domain.Entities;

public class Tag : EntityBase, IAggregateRoot, IAuditable<string>
{
    public Tag(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public virtual List<Company> Companies { get; set; } = new ();

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }

    public string ModifiedBy { get; set; } = string.Empty;

    public DateTime ModifiedOn { get; set; }
}