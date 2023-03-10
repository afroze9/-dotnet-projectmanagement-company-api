using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.Common;

namespace ProjectManagement.CompanyAPI.Domain;

public class Tag : EntityBase, IAggregateRoot, IAuditable<int>
{
    public string Name { get; private set; }

    public virtual List<Company> Companies { get; set; } = new ();

    public Tag(string name)
    {
        Name = name;
    }
    
    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime ModifiedOn { get; set; }
}