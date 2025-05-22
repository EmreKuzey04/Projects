using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IQueryRepository<TEntity> where TEntity : BaseEntity
    {
        Task<PagedList<TEntity>> GetPagedListAsync(
         Expression<Func<TEntity, bool>> filter = null,
         bool tracking = false,
         int? pageNumber = null,
         int? pageSize = null,
         params string[] includeList

         );

        Task<IQueryable<TEntity>> GetAllAsync(
          Expression<Func<TEntity, bool>> filter = null,
          bool tracking = false,
          params string[] includeList

          );
        Task<TEntity> GetAsync(

            Expression<Func<TEntity, bool>> filter,
            bool tracking = false,
            params string[] includeList
            );
    }
}
