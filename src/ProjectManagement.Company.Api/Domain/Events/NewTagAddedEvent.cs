using ProjectManagement.CompanyAPI.Common;

namespace ProjectManagement.CompanyAPI.Domain.Events;

public class NewTagAddedEvent : DomainEventBase
{
    public NewTagAddedEvent(Company company, Tag tag)
    {
        Company = company;
        Tag = tag;
    }

    public Company Company { get; set; }

    public Tag Tag { get; set; }
}