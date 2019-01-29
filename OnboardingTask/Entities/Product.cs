using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnboardingTask.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [StringLength(100, ErrorMessage = "Product Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product Price is required.")]
        //[StringLength(500, ErrorMessage = "Category Description cannot be longer than 500 characters.")]
        public decimal Price { get; set; }

        /* Relationship. 
        public ICollection<Product> Products { get; set; }
        */
    }
}