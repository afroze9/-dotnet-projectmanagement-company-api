namespace ProjectManagement.Company.Api.Abstractions;

public interface IAuditable<TU>
{
    TU CreatedBy { get; set; }
    
    DateTime CreatedOn { get; set; }
    
    TU ModifiedBy { get; set; }
    
    DateTime ModifiedOn { get; set; }
}