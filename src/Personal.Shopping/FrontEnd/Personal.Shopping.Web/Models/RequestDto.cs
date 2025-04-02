using Personal.Shopping.Web.Models.Enums;

namespace Personal.Shopping.Web.Models;

public class RequestDto
{
    public object? Content { get; set; }
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
}
