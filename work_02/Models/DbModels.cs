using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace work_02.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required,StringLength(50),Display(Name ="Customer Name")]
        public string CustomerName { get; set; }
        [Required, StringLength(50), Display(Name = "Customer Address")]
        public string Address { get; set; }
        [Required, StringLength(50), Display(Name = "Post Code")]
        public string PostCode { get; set; }
        [Required, Display(Name = "Date Of Birth")]
        [Column(TypeName ="date"),DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime DateOfBirth { get; set; }
        [Required,Display(Name ="Marital Status")]
        public bool IsMarried { get; set; }
        //fk
        [ForeignKey("Country")]
        public int CountryId { get; set; }

        //nev
        public virtual Country Country { get; set; }
        public virtual CustomerPhoto CustomerPhoto { get; set; }

    }
    public class Country
    {
        public int CountryId { get; set; }
        [Required, Display(Name = "Country Name")]
        public string CountryName { get; set; }

        //nev
        public virtual ICollection<Customer> Customers { get; set; }
    }
    public class CustomerPhoto
    {
        [Key,ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public string Image { get; set; }
        //nev
        public virtual Customer Customer { get; set; }

    }
    public class CustomerDbContext:DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CustomerPhoto> customerPhotos { get; set; }


    }

}
