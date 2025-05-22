using Application.Abstractions.Services.InMemoryCache;
using Application.Models.Dtos.Shippers;
using Application.Models.ResponseWrappers;
using AutoMapper;
using Domain.Interfaces.Repositories.Shippers;
using MediatR;

namespace Application.Features.Shippers.Queries.GetShippersByDeliveryTime
{
    public class GetShippersByDeliveryTimeQuery : IRequest<Response<List<ShipperGetDto>>>, ICachableQuery
    {
        public int MinDays { get; set; }
        public int MaxDays { get; set; }

        string ICachableQuery.CacheKey => $"Shippers_GetShippersByDeliveryTime&MinDays:{MinDays}&MaxDays:{MaxDays}";
        double ICachableQuery.CacheTime => 60;

        public class GetShippersByDeliveryTimeQueryHandler : IRequestHandler<GetShippersByDeliveryTimeQuery, Response<List<ShipperGetDto>>>
        {
            private readonly IShipperQueryRepository _shipperQueryRepository;
            private readonly IMapper _mapper;

            public GetShippersByDeliveryTimeQueryHandler(IShipperQueryRepository shipperQueryRepository, IMapper mapper)
            {
                _shipperQueryRepository = shipperQueryRepository;
                _mapper = mapper;
            }

            public async Task<Response<List<ShipperGetDto>>> Handle(GetShippersByDeliveryTimeQuery request, CancellationToken cancellationToken)
            {
                var shippers = await _shipperQueryRepository.GetByDeliveryTimeRangeAsync(
                    minDays: request.MinDays,
                    maxDays: request.MaxDays
                );

                var mapped = _mapper.Map<List<ShipperGetDto>>(shippers);
                return new Response<List<ShipperGetDto>>(mapped);
            }
        }
    }
}
