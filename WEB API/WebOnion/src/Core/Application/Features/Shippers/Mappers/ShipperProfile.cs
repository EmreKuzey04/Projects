using Application.Models.Dtos.Shippers;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shippers.Mappers
{
    public class ShipperProfile:Profile
    {
        public ShipperProfile() 
        {
            CreateMap<Shipper, ShipperGetDto>();
             
        }
    }
}
