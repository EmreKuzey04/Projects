using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product:BaseEntity
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        public int? UnitsInStock { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Description { get; set; }
        public byte[]? Photo { get; set; }

        public Category? Category { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
