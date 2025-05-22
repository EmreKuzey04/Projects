using Application.Abstractions.Services.InMemoryCache;
using Application.Models.Dtos.Shippers;
using Application.Models.ResponseWrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories.Shippers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shippers.Commands.CreateShipper
{
    public class CreateShipperCommand:IRequest<Response<ShipperGetDto>>
    {
        public int ShipperId { get; set; }
        public string ShipperName { get; set; }
        public string DeliveryTime {  get; set; }
        public string Phone { get; set; }

        public class CreateShipperCommandHandler : IRequestHandler<CreateShipperCommand, Response<ShipperGetDto>>
        {
            private readonly IShipperCommandRepository _shipperCommandRepository;
            private readonly IMapper _mapper;
            private readonly ICacheService _cacheService;

            public CreateShipperCommandHandler(IShipperCommandRepository shipperCommandRepository, IMapper mapper, ICacheService cacheService)
            {
                _shipperCommandRepository = shipperCommandRepository;
                _mapper = mapper;
                _cacheService = cacheService;
            }

            public async Task<Response<ShipperGetDto>> Handle(CreateShipperCommand request, CancellationToken cancellationToken)
            {
                Shipper shipper = new Shipper
                {
                    ShipperId = request.ShipperId,
                    ShipperName = request.ShipperName,
                    DeliveryTime = request.DeliveryTime,
                    Phone = request.Phone
                };

                await _shipperCommandRepository.AddAsync(shipper);

                var mapped = _mapper.Map<ShipperGetDto>(shipper);

                return new Response<ShipperGetDto>(mapped);
            }
        }
    }
}
