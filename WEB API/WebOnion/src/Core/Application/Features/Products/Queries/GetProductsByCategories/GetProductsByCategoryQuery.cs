using Application.Exceptions;
using Application.Models.Dtos;
using Application.Models.Parameters;
using Application.Models.ResponseWrappers;
using AutoMapper;
using Domain.Interfaces.Repositories.Products;
using MediatR;

namespace Application.Features.Products.Queries.GetProductsByCategories
{
    public class GetProductsByCategoryQuery:QueryParameters,IRequest<PagedResponse<List<ProductGetDto>>>
    {
        public int CategoryId { get; set; }


        public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, PagedResponse<List<ProductGetDto>>>
        {
            private readonly IProductQueryRepository _productQueryRepository;
            private readonly IMapper _mapper;
            public GetProductsByCategoryQueryHandler(IProductQueryRepository productQueryRepository, IMapper mapper)
            {
                _productQueryRepository = productQueryRepository;
                _mapper = mapper;
            }
            public async Task<PagedResponse<List<ProductGetDto>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
            {
                var query =
                    await _productQueryRepository.GetPagedListAsync(

                     filter: x => x.IsActive.Value && !x.IsDeleted.Value && x.CategoryID == request.CategoryId,
                     includeList: "Category",
                     pageNumber: request.Page,
                     pageSize: request.PageSize

                    );

                var totalPagesCount = (int)Math.Ceiling(query.TotalCount / (double)request.PageSize);

                if (request.Page > totalPagesCount)
                {
                    throw new BadRequestException("Bu sayfada kayıt bulunmamaktadır. Lütfen bir önceki sayfayı deneyin ");
                }

               var mapped = _mapper.Map<List<ProductGetDto>>(query.Items.ToList());

                var response = new PagedResponse<List<ProductGetDto>>(mapped,query.TotalCount,request.Page.Value,request.PageSize.Value);
                return response;
            }
        }

    }

    
}
