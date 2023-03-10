namespace ProjectManagement.CompanyAPI.Model;

public class CompanyResponseModel
{
    public string Id { get; set; }
    
    public string Name { get; private set; }

    public virtual List<TagResponseModel> Tags { get; set; } = new ();
}

public class TagResponseModel
{
    public int Id { get; set; }
    
    public string Name { get; set; }
}