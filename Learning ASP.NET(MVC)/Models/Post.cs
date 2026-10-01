using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Learning_ASP.NET_MVC_.Models
{
    [Table("Tbl_Post")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(400, ErrorMessage = "Title cannot exceed 400 characters")]
        public string? Title { get; set; }
        
        [Required(ErrorMessage = "Content is required")]
        public string? Content { get; set; }
        
        [Required(ErrorMessage = "author is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string? Author { get; set; }
        
        //[Required(ErrorMessage = "Date is required")]
        [ValidateNever]
        public string? FeaturedImage { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;
        
        [ForeignKey("Category")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        
        // Navigation Property ទៅកាន់ Category
        [ValidateNever]
        public Category Category { get; set; } = null!;

        // Navigation Property (1 Post មាន Comments ច្រើន)
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
