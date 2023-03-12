namespace ProjectManagement.CompanyAPI.Model;

public class CompanySummaryResponseModel
{
    public int Id { get; set; }

    required public string Name { get; set; }

    public List<string> Tags { get; set; } = new ();
}