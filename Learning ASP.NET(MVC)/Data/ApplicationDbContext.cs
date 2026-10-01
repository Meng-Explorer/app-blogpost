using Learning_ASP.NET_MVC_.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Learning_ASP.NET_MVC_.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1 ,Name = "Technology"},
                new Category { Id = 2 ,Name = "Healthy"},
                new Category { Id = 3 ,Name = "LifStyle"}
                );
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Title = "Tech Post 1 ",
                    Content = "This is the Tech Post 1 Content",
                    Author = "VireakBoth",
                    PublishDate = new DateTime(2026,9,28),
                    CategoryId = 1,
                    FeaturedImage = "tech_image.jpg"
                },
                new Post
                {
                    Id = 2,
                    Title = "Health Post 2 ",
                    Content = "This is the Health Post 2 Content",
                    Author = "MengLy",
                    PublishDate = new DateTime(2026,9,28),
                    CategoryId = 2,
                    FeaturedImage = "tech_image.jpg"
                },
                new Post
                {
                    Id = 3,
                    Title = "LifeStyle Post 3 ",
                    Content = "This is the LifeStyle Post 3 Content",
                    Author = "MengLang",
                    PublishDate = new DateTime(2026,9,28),
                    CategoryId = 3,
                    FeaturedImage = "health_image.jpg"
                }
                );

        }
    }
}

