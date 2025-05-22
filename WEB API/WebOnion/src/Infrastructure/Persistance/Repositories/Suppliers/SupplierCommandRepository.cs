using Domain.Entities;
using Domain.Interfaces.Repositories.Suppliers;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories.Suppliers
{
    public class SupplierCommandRepository:CommandRepository<Supplier>,ISupplierCommandRepository
    {
        public SupplierCommandRepository(TradewndContext context):base(context) 
        {
            
        }
        
            
        


    }
}
