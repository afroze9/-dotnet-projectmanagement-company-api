namespace ProjectManagement.CompanyAPI.Model;

public class CompanyRequestModel
{
    required public string Name { get; set; }

    public List<string> Tags { get; set; } = new ();
}