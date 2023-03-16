namespace ProjectManagement.CompanyAPI.Model;

[ExcludeFromCodeCoverage]
public class CompanyResponseModel
{
    public int Id { get; set; }

    required public string Name { get; set; }

    public int ProjectCount { get; set; }
    public virtual List<TagResponseModel> Tags { get; set; } = new ();
}