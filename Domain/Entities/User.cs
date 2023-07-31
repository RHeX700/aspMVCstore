using Microsoft.AspNetCore.Identity;
namespace Domain.Entities;
public class User : IdentityUser
{
    [PersonalData]
    public string? Name { get; set; }
    [PersonalData]
    public string Address { get; set; }
    public ICollection<Order>? Orders { get; set; }
}