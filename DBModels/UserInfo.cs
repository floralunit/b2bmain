using B2BWebService.ResponseRequestModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2BWebService.DBModels;

public class UserInfo
{
    public string? UserFullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}
