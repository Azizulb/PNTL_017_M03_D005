using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_01_with_Image.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Unit { get; set; }
        [Required,Range(1,10000)]
        public double Price { get; set; }
        public byte[] Image { get; set; }
        public string ImageFile { get; set; }

    }
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options):base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

    }
}
