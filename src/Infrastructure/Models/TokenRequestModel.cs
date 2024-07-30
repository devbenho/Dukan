using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;


    public abstract class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
