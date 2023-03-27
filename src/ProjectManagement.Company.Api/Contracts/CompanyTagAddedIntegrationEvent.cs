namespace ProjectManagement.CompanyAPI.Contracts;

public record CompanyTagAddedIntegrationEvent(int CompanyId, string TagName) : IntegrationEvent;