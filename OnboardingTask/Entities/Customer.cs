using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnboardingTask.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Name is required.")]
        [StringLength(100, ErrorMessage = "Customer Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Address cannot be longer than 500 characters.")]
        public string Address { get; set; }

        /* Relationship. 
        public ICollection<Product> Products { get; set; }
        */
    }
}