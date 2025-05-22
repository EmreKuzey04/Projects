using Domain.Entities;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.Products
{
    public interface IProductQueryRepository : IQueryRepository<Product>
    { 
        Task<IQueryable<Product>> GetByPriceAsync(
            
            decimal min,
            decimal max,
            params string[] includeList
            
            
            );

        Task<PagedList<Product>> GetByPricePagedAsync(

          decimal min,
          decimal max,
          int pageNumber,
          int pageSize,
          params string[] includeList


          );

    }
}
