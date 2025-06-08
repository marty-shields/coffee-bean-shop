namespace Coffee.Bean.Shop.Api.Models;

public class ErrorResponseModel
{
    public string Error { get; init; }

    public ErrorResponseModel(string error)
    {
        Error = error;
    }
}
