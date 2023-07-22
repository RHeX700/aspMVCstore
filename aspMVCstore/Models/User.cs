using Microsoft.AspNetCore.Identity;
namespace aspMVCstore.Models;
public class User : IdentityUser
{
    [PersonalData]
    public string? Name { get; set; }
    [PersonalData]
    public string Address { get; set; }
    public ICollection<Order>? Orders { get; set; }
}