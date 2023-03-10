using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.Common;
using ProjectManagement.CompanyAPI.Domain.Events;

namespace ProjectManagement.CompanyAPI.Domain;

public class Company : EntityBase, IAggregateRoot, IAuditable<string>
{
    public string Name { get; private set; }

    public virtual List<Tag> Tags { get; set; } = new ();

    public Company(string name)
    {
        Name = name;
    }

    public void AddTag(Tag tag)
    {
        if (tag == null)
        {
            throw new ArgumentNullException(nameof(tag));
        }
        
        Tags.Add(tag);
        var newTagAddedEvent = new NewTagAddedEvent(this, tag);
        RegisterDomainEvent(newTagAddedEvent);
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrEmpty(newName))
        {
            throw new ArgumentNullException(nameof(newName));
        }
        
        Name = newName;
    }
    
    public string CreatedBy { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }

    public string ModifiedBy { get; set; } = string.Empty;

    public DateTime ModifiedOn { get; set; }
}