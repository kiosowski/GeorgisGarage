using GeorigsGarage.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeorgisGarage.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CoverPhotoService> CoverPhotoService { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartProduct> CartProducts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Reply> Replies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CartProduct>().HasKey(x => new { x.ProductId, x.CartId });

            builder.Entity<Service>()
                .HasOne(x => x.CoverPhoto)
                .WithOne(x => x.Service)
                .HasForeignKey<CoverPhotoService>(x => x.Id);
            builder.Entity<Service>()
                .HasMany(x => x.Photos)
                .WithOne(x => x.Service)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Service>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Service)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasMany(x => x.Replies)
                .WithOne(x => x.Comment)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderProduct>().HasKey(x => new { x.OrderId, x.ProductId });

            builder.Entity<Product>()
                .HasOne(x => x.Image)
                .WithOne(x => x.Product)
                .HasForeignKey<ProductImage>(x => x.Id);

            builder.Entity<Cart>()
                .HasOne(x => x.User)
                .WithOne(x => x.Cart)
                .HasForeignKey<User>(x => x.CartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Service>()
                .Property(x => x.AverageRating)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}

