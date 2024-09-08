using System.ComponentModel.DataAnnotations;
using static System.String;

namespace Infrastructure.Model;

public class RegisterViewModel
{
 [Required] [EmailAddress] public string Email { get; set; } = Empty;

 [Required]
 [DataType(DataType.Password)]
 public string Password { get; set; } = Empty;

 [DataType(DataType.Password)]
 [Display(Name = "Confirm password")]
 [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
 public string ConfirmPassword { get; set; } = Empty;
}