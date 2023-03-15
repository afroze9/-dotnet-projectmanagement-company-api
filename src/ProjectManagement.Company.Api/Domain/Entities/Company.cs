using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.Common;
using ProjectManagement.CompanyAPI.Domain.Events;

namespace ProjectManagement.CompanyAPI.Domain.Entities;

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
        if (!Tags.Contains(tag))
        {
            Tags.Add(tag);
            NewTagAddedEvent newTagAddedEvent = new (this, tag);
            RegisterDomainEvent(newTagAddedEvent);
        }
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
        Name = newName;
    }

    public void RemoveTag(string tagName)
    {
        Tag? tagToRemove = Tags.FirstOrDefault(x => x.Name == tagName);

        if (tagToRemove != null)
        {
            Tags.Remove(tagToRemove);
            TagRemovedEvent @event = new (this, tagToRemove);
            RegisterDomainEvent(@event);
        }
    }

    public void RemoveTags()
    {
        if (Tags.Count == 0)
        {
            return;
        }

        List<string> tagNames = Tags.Select(t => t.Name).ToList();

        foreach (string tagName in tagNames)
        {
            RemoveTag(tagName);
        }
    }
}