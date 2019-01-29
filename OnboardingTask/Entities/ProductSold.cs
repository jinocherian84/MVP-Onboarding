using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnboardingTask.Entities
{
    public class ProductSold
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int DateSold { get; set; }
        /* Relationship. 
         public ICollection<Product> Products { get; set; }
         */
    }
}