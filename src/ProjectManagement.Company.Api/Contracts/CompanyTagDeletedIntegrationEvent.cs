namespace ProjectManagement.CompanyAPI.Contracts;

public record CompanyTagDeletedIntegrationEvent(int CompanyId, string TagName) : IntegrationEvent;