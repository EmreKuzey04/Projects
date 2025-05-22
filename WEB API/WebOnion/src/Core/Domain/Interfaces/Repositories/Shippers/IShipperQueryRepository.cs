using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.Shippers
{
    public interface IShipperQueryRepository : IQueryRepository<Shipper>
    {
        Task<IQueryable<Shipper>> GetByDeliveryTimeRangeAsync(
            int minDays,
            int maxDays,
            params string[] includeList
        );
    }
}
