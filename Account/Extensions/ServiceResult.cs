namespace Account.Extensions;

//todo move all file to another place
public class ServiceResult<T>
{
    public ServiceResult()
    {

    }

    public ServiceResult(T data)
    {
        Data = data;
    }
    public ServiceResult(ClientErrorType type, string errorMessage)
    {
        var clientError = new ClientError { ErrorType = type, ErrorMessage = errorMessage };
        Error = clientError;
    }
    public T? Data { get; set; }
    public ClientError? Error { get; set; }
}


public class ClientError
{
    public ClientErrorType ErrorType { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}

public enum ClientErrorType
{
    BadRequest,
    Unauthorized,
    Forbidden,
    NotFound,
    Conflict
}