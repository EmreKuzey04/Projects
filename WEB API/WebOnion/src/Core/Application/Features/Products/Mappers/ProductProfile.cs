using Application.Models.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Mappers
{
    public class ProductProfile:Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductGetDto>()
                .ForMember(

                dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.CategoryName));
        
        }
    }
}
