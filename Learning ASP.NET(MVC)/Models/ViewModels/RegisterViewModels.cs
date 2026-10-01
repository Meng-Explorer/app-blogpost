using System.ComponentModel.DataAnnotations;

namespace Learning_ASP.NET_MVC_.Models.ViewModels
{
    public class RegisterViewModels
    {
        [Required(ErrorMessage = "Email is Required ")]
        [EmailAddress(ErrorMessage = "Email must be in proper format")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Password is Required ")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}

