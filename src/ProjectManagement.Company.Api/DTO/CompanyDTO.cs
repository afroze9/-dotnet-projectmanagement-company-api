namespace ProjectManagement.CompanyAPI.DTO;

public class CompanyDTO
{
    public int Id { get; set; }

    required public string Name { get; set; }

    public List<TagDTO> Tags { get; set; } = new ();
}