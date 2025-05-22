using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category:BaseEntity
    {
        public Category() 
        {
            Products = new HashSet<Product>();
        }
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public byte[]? Photo { get; set; }
        public ICollection<Product>Products { get; set; }

    }
}
