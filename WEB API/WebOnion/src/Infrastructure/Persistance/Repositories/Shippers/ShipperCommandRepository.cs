using Domain.Entities;
using Domain.Interfaces.Repositories.Shippers;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories.Shippers
{
    public class ShipperCommandRepository:CommandRepository<Shipper>,IShipperCommandRepository
    {
        public ShipperCommandRepository(TradewndContext context) : base(context)
        {

        }
    }
}
