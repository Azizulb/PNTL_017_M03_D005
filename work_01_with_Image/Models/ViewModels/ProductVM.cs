using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace work_01_with_Image.Models.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Unit { get; set; }
        [Required, Range(1, 10000)]
        public double Price { get; set; }
        public IFormFile Image { get; set; }

    }
}
