using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppAspLayered.Models.Users;

public class RegisterFormDto
{
    [Required]
    [MaxLength(150)]
    [DisplayName("Email")]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [DisplayName("Mot de passe")]
    public string Password { get; set; } = null!;

    [Required]
    [Compare("Password", ErrorMessage = "FDP!! Met le bon MDP...")]
    [DataType(DataType.Password)]
    [DisplayName("Mot de passe de confirmation")]
    public string ConfirmPassword { get; set;} = null!;
}
