using Application.Abstractions.Services.InMemoryCache;
using Application.Features.Products.Queries.GetAllActiveProducts;
using Application.Models.Dtos;
using Application.Models.Dtos.Shippers;
using Application.Models.ResponseWrappers;
using AutoMapper;
using Domain.Interfaces.Repositories.Products;
using Domain.Interfaces.Repositories.Shippers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shippers.Queries.GetAllShippers
{
    public class GetAllShippersQuery : IRequest<Response<List<ShipperGetDto>>>, ICachableQuery
    {
        string ICachableQuery.CacheKey
        {
            get
            {
                return "Shippers_GetAllShippers";
            }
        }

        double ICachableQuery.CacheTime
        {
            get { return 60; }
        }

        public class GetAllShippersQueryHandler : IRequestHandler<GetAllShippersQuery, Response<List<ShipperGetDto>>>
        {
            private readonly IShipperQueryRepository _shipperQueryRepository;
            private readonly IMapper _mapper;
            public GetAllShippersQueryHandler(IShipperQueryRepository shipperQueryRepository, IMapper mapper)
            {
                _shipperQueryRepository = shipperQueryRepository;
                _mapper = mapper;
            }
            public async Task<Response<List<ShipperGetDto>>> Handle(GetAllShippersQuery request, CancellationToken cancellationToken)
            {
                var shippers = await _shipperQueryRepository.GetAllAsync();

                var mapped = _mapper.Map<List<ShipperGetDto>>(shippers);

                var response = new Response<List<ShipperGetDto>>(mapped);

                return response;
            }
        }
    }
}
