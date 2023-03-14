namespace ProjectManagement.CompanyAPI.Configuration;

[ExcludeFromCodeCoverage]
public class PersistenceSettings
{
    required public string ConnectionString { get; set; }
}