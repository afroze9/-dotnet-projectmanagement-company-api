using ProjectManagement.Company.Api.Common;

namespace ProjectManagement.Company.Api.Domain.Events;

public class NewTagAddedEvent : DomainEventBase
{
    public Company Company { get; set; }
    
    public Tag Tag { get; set; }
    
    public NewTagAddedEvent(Company company, Tag tag)
    {
        Company = company;
        Tag = tag;
    }
}