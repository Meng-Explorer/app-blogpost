using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning_ASP.NET_MVC_.Models
{
    [Table("Tbl_Comments")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please enter Comment Content")]
        [MaxLength(200, ErrorMessage = "Comment Content must be 200 characters or less")]
        public string? Username  { get; set; }
        
        [Required]
        public string? Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime CommentDate { get; set; } = DateTime.Now;
        
        [ForeignKey("Post")]
        public int PostId { get; set; }
        
        // Navigation Property ទៅកាន់ Post
        public Post Post { get; set; } = null!;
    }
}

