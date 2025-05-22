using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Supplier:BaseEntity
    {
        public Supplier() 
        {
            Products = new HashSet<Product>();
        }
        public int SupplierID { get; set; }
        public string? SupplierName { get; set; }
        public string? ContactName { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
