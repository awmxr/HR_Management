﻿using System.ComponentModel.DataAnnotations;

namespace HR_Management.MVC.Models
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
