using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Dtos.Shippers
{
    public class ShipperGetDto
    {
        public int ShipperId { get; set; }
        public string? ShipperName { get; set; }
        public string? DeliveryTime { get; set; }
        public string? Phone {  get; set; }

    }
}
