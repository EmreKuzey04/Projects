using Domain.Entities;
using Domain.Interfaces.Repositories.Shippers;
using Persistance.Contexts;
using System.Linq;
using System.Threading.Tasks;

namespace Persistance.Repositories.Shippers
{
    public class ShipperQueryRepository : QueryRepository<Shipper>, IShipperQueryRepository
    {
        public ShipperQueryRepository(TradewndContext context) : base(context)
        {
        }

        public async Task<IQueryable<Shipper>> GetByDeliveryTimeRangeAsync(int minDays, int maxDays, params string[] includeList)
        {
            return await GetAllAsync(
                filter: shp => shp.DeliveryTimeStart >= minDays && shp.DeliveryTimeEnd <= maxDays,
                includeList: includeList
            );
        }
    }
}
