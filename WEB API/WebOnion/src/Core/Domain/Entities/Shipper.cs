using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
     public class Shipper:BaseEntity
     {
        public int ShipperId { get; set; }
        public string? ShipperName { get; set; }
        public string? DeliveryTime { get; set; }
        public int? DeliveryTimeStart { get; set; }
        public int? DeliveryTimeEnd { get; set; }
        public string? Phone { get; set; }
    }
} 
