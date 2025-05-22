using Application.Abstractions.Services.InMemoryCache;
using Application.Models.Dtos;
using Application.Models.ResponseWrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand:IRequest<Response<ProductGetDto>>
    {

        public string ProductName { get; set; }
        public int UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int? CategoryId { get; set; }
        public int SupplierId { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<ProductGetDto>>
        {
          private readonly IProductCommandRepository _productCommandRepository;
          private readonly IMapper _mapper;
          private readonly ICacheService _cacheService;

            public CreateProductCommandHandler(IProductCommandRepository productCommandRepository, IMapper mapper, ICacheService cacheService)
            {
                _productCommandRepository = productCommandRepository;
                _mapper = mapper;
                _cacheService = cacheService;
            }
            public async Task<Response<ProductGetDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                Product product = new Product
                {
                    //CategoryID = request.CategoryId,
                    ProductName = request.ProductName,
                    UnitPrice = request.UnitPrice,
                    UnitsInStock = request.UnitsInStock,
                    SupplierID = request.SupplierId,
                    IsActive = true,
                    IsDeleted = false

                };

                await _productCommandRepository.AddAsync(product);

               var mapped = _mapper.Map<ProductGetDto>(product);



                //_cacheService.Remove();


                return new Response<ProductGetDto>(mapped);
            }
        }
    }
}
