using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DTOs.Stock.Account
{
    public class RegisterDto
    {
        [Required]
        public string? Username { get; set;}
        [Required]
        public string? Lastname { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set;}

        [Required]
        [MinLength(12, ErrorMessage = "Password requires a minimum of 12 characters.")]
        public string? Password { get; set;}
        
    }
}