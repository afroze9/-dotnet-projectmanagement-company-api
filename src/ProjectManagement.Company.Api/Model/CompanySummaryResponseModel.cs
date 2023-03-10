namespace ProjectManagement.CompanyAPI.Model;

public class CompanySummaryResponseModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<string> Tags { get; set; }
}