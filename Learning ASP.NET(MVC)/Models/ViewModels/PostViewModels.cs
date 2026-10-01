using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Learning_ASP.NET_MVC_.Models.ViewModels;

public class PostViewModels
{
    public Post? PostData { get; set; } 
    
    [ValidateNever]
    public IEnumerable<SelectListItem>?  CategoryList { get; set; }
    public IFormFile? ImageFiles { get; set; }
}