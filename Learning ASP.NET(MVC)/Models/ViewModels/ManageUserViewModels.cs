using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Learning_ASP.NET_MVC_.Models.ViewModels
{
    public class ManageUserViewModels
    {
        public string? Id { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string? RoleId { get; set; } 

        public IEnumerable<SelectListItem>? RoleList { get; set; }
    }
}