using Application.Abstractions.Services.InMemoryCache;
using Application.Exceptions;
using Application.Features.Products.Queries.GetProductsByCategories;
using Application.Models.Dtos;
using Application.Models.ResponseWrappers;
using AutoMapper;
using Domain.Interfaces.Repositories.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAllActiveProducts
{
    public class GetAllProductsQuery : IRequest<Response<List<ProductGetDto>>>, ICachableQuery
    {
        string ICachableQuery.CacheKey
        {
            get
            {
                return "Products_GetAllActiveProducts";
            }
        }

        double ICachableQuery.CacheTime
        {
            get { return 60; }
        }

        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery,Response<List<ProductGetDto>>>
        {
            private readonly IProductQueryRepository _productQueryRepository;
            private readonly IMapper _mapper;
            public GetAllProductsQueryHandler(IProductQueryRepository productQueryRepository, IMapper mapper)
            {
                _productQueryRepository = productQueryRepository;
                _mapper = mapper;
            }
            public async Task<Response<List<ProductGetDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
               var products = await _productQueryRepository.GetAllAsync(includeList: "Category");

               var mapped = _mapper.Map<List<ProductGetDto>>(products);

                var response = new Response<List<ProductGetDto>>(mapped);

                return response;
            }
        }
    }
}
