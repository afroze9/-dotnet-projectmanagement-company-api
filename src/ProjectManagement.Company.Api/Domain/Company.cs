using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.Common;
using ProjectManagement.CompanyAPI.Domain.Events;

namespace ProjectManagement.CompanyAPI.Domain;

public class Company : EntityBase, IAggregateRoot, IAuditable<string>
{
    public Company(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public virtual List<Tag> Tags { get; set; } = new ();

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }

    public string ModifiedBy { get; set; } = string.Empty;

    public DateTime ModifiedOn { get; set; }

    public void AddTag(Tag tag)
    {
        if (tag == null)
        {
            throw new ArgumentNullException(nameof(tag));
        }

        Tags.Add(tag);
        NewTagAddedEvent newTagAddedEvent = new (this, tag);
        RegisterDomainEvent(newTagAddedEvent);
    }

    public void AddTags(List<Tag> tags)
    {
        foreach (Tag tag in tags)
        {
            AddTag(tag);
        }
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrEmpty(newName))
        {
            throw new ArgumentNullException(nameof(newName));
        }

        Name = newName;
    }
}