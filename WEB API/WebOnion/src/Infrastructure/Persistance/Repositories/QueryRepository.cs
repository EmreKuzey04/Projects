using Domain.Interfaces;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public abstract class QueryRepository<TEntity> : IQueryRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public QueryRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, bool tracking = false, params string[] includeList)
        {
            IQueryable<TEntity> query = _dbSet;

            if(!tracking)
                query = query.AsNoTracking();

            if(filter != null)
                query = query.Where(filter);
            if(includeList != null && includeList.Length > 0)
            {
                foreach(var item in includeList)
                {
                    query = query.Include(item);
                }
            }
            return query;


        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracking = false, params string[] includeList)
        {
            IQueryable<TEntity> query = _dbSet;

            if (!tracking)
                query = query.AsNoTracking();

            if (includeList != null && includeList.Length > 0)
            {
                foreach (var item in includeList)
                {
                    query = query.Include(item);
                }
            }

           
               return await query.SingleOrDefaultAsync();
            
        }

        public async Task<PagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> filter = null, bool tracking = false, int? pageNumber = null, int? pageSize = null, params string[] includeList)
        {
           IQueryable<TEntity> query = _dbSet;
            var pagedList = new PagedList<TEntity>();

            if(!tracking)
                query = query.AsNoTracking();

            if(filter != null)
                query = query.Where(filter);

            if (includeList != null && includeList.Length > 0)
            {
                foreach (var item in includeList)
                {
                    query=query.Include(item);
                }

            }
            pagedList.TotalCount= query.Count();

            if(pageNumber.HasValue && pageSize.HasValue)
              query = query.Skip((pageNumber.Value -1 ) * pageSize.Value).Take(pageSize.Value);

            pagedList.Items= query;

            return pagedList;
        }  
    }
}
