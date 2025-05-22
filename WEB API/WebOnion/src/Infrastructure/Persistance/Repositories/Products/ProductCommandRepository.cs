using Domain.Entities;
using Domain.Interfaces.Repositories.Products;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories.Products
{
    public class ProductCommandRepository:CommandRepository<Product>,IProductCommandRepository
    {
        public ProductCommandRepository(TradewndContext context):base(context) 
        { 

        }
        
    }
}
