using Application.Features.Products.Queries.GetProductsByCategories;
using Domain.Interfaces.Repositories.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Validators
{
    public class GetProductsByCategoryQueryValidator:AbstractValidator<GetProductsByCategoryQuery>
    {
        public GetProductsByCategoryQueryValidator() 
        {
            RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("Kategori Boş Bırakılamaz")
            .GreaterThan(0)
            .WithMessage("Kategori Id Değeri Sıfırdan Büyük Olmalıdır");


            RuleFor(x => x.Page)
                .NotEmpty()
                .WithMessage("Sayfa Bilgisi Boş Bırakılamaz")
                .GreaterThan(0)
                .WithMessage("Sayfa Numara Değeri Sıfırdan Büyük Olmalıdır");
               

            RuleFor(x=> x.PageSize)
                 .NotEmpty()
                 .WithMessage("Sayfa Kayıt Bilgisi Boş Bırakılamaz")
                 .GreaterThan(0)
                 .WithMessage("Sayfa Kayıt Numara Değeri Sıfırdan Büyük Olmalıdır");
        }

    }
}
