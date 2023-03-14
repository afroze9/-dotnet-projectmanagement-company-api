using ProjectManagement.CompanyAPI.Common;
using ProjectManagement.CompanyAPI.Domain.Entities;

namespace ProjectManagement.CompanyAPI.Domain.Events;

public class TagRemovedEvent : DomainEventBase
{
    public TagRemovedEvent(Company company, Tag tag)
    {
        Company = company;
        Tag = tag;
    }

    public Company Company { get; set; }

    public Tag Tag { get; set; }
}