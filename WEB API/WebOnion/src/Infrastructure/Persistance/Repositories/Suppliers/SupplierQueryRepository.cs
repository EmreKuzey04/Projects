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
    public class SupplierQueryRepository:QueryRepository<Supplier>,ISupplierQueryRepository
    {
        public SupplierQueryRepository(TradewndContext context):base(context) 
        {
            
        }
        

        
        
           
        
    }
}
