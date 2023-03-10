namespace ProjectManagement.CompanyAPI.Model;

public class HttpResponseModel<T>
{
    public T Data { get; private set; }
    
    public bool IsError { get; private set; }
    
    public string ErrorMessage { get; private set; }

    private HttpResponseModel(T data, bool isError, string errorMessage)
    {
        Data = data;
        IsError = isError;
        ErrorMessage = errorMessage;
    }

    public static HttpResponseModel<T> Success(T data)
    {
        return new HttpResponseModel<T>(data, false, string.Empty);
    }

    public static HttpResponseModel<T> Failure(T data, string errorMessage)
    {
        return new HttpResponseModel<T>(data, true, errorMessage);
    }
}