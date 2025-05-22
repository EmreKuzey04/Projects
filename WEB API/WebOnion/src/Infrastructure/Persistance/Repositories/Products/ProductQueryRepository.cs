using Domain.Entities;
using Domain.Interfaces.Repositories.Products;
using Domain.Shared;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories.Products
{
    public class ProductQueryRepository : QueryRepository<Product>, IProductQueryRepository
    {
        public ProductQueryRepository(TradewndContext context):base(context) 
        {
            
        }   
        public async Task<IQueryable<Product>> GetByPriceAsync(decimal min, decimal max, params string[] includeList)
        {
            return await GetAllAsync(
                filter: prd => prd.UnitPrice > min && prd.UnitPrice < max,
                includeList: includeList

                );
        }

        public async Task<PagedList<Product>> GetByPricePagedAsync(decimal min, decimal max,int pageNumber,int pageSize, params string[] includeList)
        {
            return await GetPagedListAsync(
              filter: prd => prd.UnitPrice > min && prd.UnitPrice < max,
              includeList: includeList,
              pageNumber: pageNumber,
              pageSize: pageSize

              );
        }
    }
}
