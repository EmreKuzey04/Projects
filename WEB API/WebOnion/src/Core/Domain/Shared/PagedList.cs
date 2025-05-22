using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
    public class PagedList<TEntity> where TEntity : BaseEntity
    {
        public IQueryable<TEntity> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
