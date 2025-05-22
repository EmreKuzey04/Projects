using Application.Abstractions.Services.InMemoryCache;
using Application.Exceptions;
using Application.Models.Dtos;
using Application.Models.Parameters;
using Application.Models.ResponseWrappers;
using AutoMapper;
using Domain.Interfaces.Repositories.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetProductsByPrice
{
    public class GetProductsByPriceQuery:QueryParameters,IRequest<PagedResponse<List<ProductGetDto>>>,ICachableQuery
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }

        string ICachableQuery.CacheKey
        {
            get
            {
                return $"Products_GetProductsByPrice&Min:{Min}&Max:{Max}:Page:{Page}:PageSize:{PageSize}";
            }
        }

        double ICachableQuery.CacheTime
        {
            get { return 60; }
        }

        public class GetProductsByPriceQueryHandler : IRequestHandler<GetProductsByPriceQuery, PagedResponse<List<ProductGetDto>>>
        {
            private readonly IProductQueryRepository _productQueryRepository;
            private readonly IMapper _mapper;
            public GetProductsByPriceQueryHandler(IProductQueryRepository productQueryRepository, IMapper mapper)
            {
                _productQueryRepository = productQueryRepository;
                _mapper = mapper;
            }
            public async Task<PagedResponse<List<ProductGetDto>>> Handle(GetProductsByPriceQuery request, CancellationToken cancellationToken)
            {
                var query = await
                    _productQueryRepository.GetByPricePagedAsync(

                    min: request.Min,
                    max: request.Max,
                    pageNumber: request.Page.Value,
                    pageSize: request.PageSize.Value

                    );

                var totalPagesCount = (int)Math.Ceiling(query.TotalCount / (double)request.PageSize);

                if (request.Page > totalPagesCount)
                {
                    throw new BadRequestException("Bu sayfada kayıt bulunmamaktadır. Lütfen bir önceki sayfayı deneyin ");
                }

                var mapped  = _mapper.Map<List<ProductGetDto>>(query.Items);

                return  new PagedResponse<List<ProductGetDto>>(mapped, query.TotalCount, request.Page.Value, request.PageSize.Value);
            }
        }
    }
}
