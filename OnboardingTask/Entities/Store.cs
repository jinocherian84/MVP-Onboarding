using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnboardingTask.Entities
{
    public class Store
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Store Name is required.")]
        [StringLength(100, ErrorMessage = "Store Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Address cannot be longer than 500 characters.")]
        public string Address { get; set; }

        /* Relationship. 
         public ICollection<Product> Products { get; set; }
         */
    }
}