using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Infrastructure.Models;

public abstract class RegisterModel
{
    [Required, StringLength(100)] 
    public string FirstName { get; set; } = "";

    [Required, StringLength(100)] 
    public string LastName { get; set; } = "";

    [Required, StringLength(50)] 
    public string Username { get; set; } = "";

    [Required, StringLength(128)] 
    public string Email { get; set; } = "";

    [Required, StringLength(256)] 
    public string Password { get; set; } = "";

    // Parameterless constructor
    public RegisterModel() { }

    // Parameterized constructor
    [JsonConstructor]
    public RegisterModel(string firstName, string lastName, string username, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        Email = email;
        Password = password;
    }
}