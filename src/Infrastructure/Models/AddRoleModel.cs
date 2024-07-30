using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public abstract class AddRoleModel
{
    [Required]
    public string UserId { get; set; } = "";

    [Required] 
    public string Role { get; set; } = "";
}