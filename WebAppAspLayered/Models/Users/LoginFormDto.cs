﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppAspLayered.Models.Users;

public class LoginFormDto
{
    [Required]
    [DisplayName("Email")]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [DisplayName("Mot de passe")]
    public string Password { get; set; } = null!;
}
