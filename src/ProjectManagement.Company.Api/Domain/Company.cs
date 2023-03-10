using ProjectManagement.Company.Api.Abstractions;
using ProjectManagement.Company.Api.Common;
using ProjectManagement.Company.Api.Domain.Events;

namespace ProjectManagement.Company.Api.Domain;

public class Company : EntityBase, IAggregateRoot, IAuditable<string>
{
    public string Name { get; private set; }

    private readonly List<Tag> _tags = new ();

    public IEnumerable<Tag> Tags => _tags.AsReadOnly();
    
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
        
        _tags.Add(tag);
        var newTagAddedEvent = new NewTagAddedEvent(this, tag);
        base.RegisterDomainEvent(newTagAddedEvent);
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