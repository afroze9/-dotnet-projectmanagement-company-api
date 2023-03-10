using ProjectManagement.Company.Api.Abstractions;
using ProjectManagement.Company.Api.Common;

namespace ProjectManagement.Company.Api.Domain;

public class Tag : EntityBase, IAuditable<int>
{
    public string Name { get; private set; }

    public Tag(string name)
    {
        Name = name;
    }
    
    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime ModifiedOn { get; set; }
}