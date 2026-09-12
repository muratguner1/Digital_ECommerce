using System.ComponentModel.DataAnnotations;

namespace Digital_Persistence_Layer.Model;

public class RegisterModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}