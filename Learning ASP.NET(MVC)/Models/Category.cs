using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning_ASP.NET_MVC_.Models
{
    [Table("Tbl_Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(100,ErrorMessage = "Category name cannot exceed 100 characters")]
        public string? Name { get; set; }
        
        // [Required(ErrorMessage = "Category description is required")]
        public string? Description { get; set; }
        
        // Navigation Property (1 Category មាន Posts ច្រើន)
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}

