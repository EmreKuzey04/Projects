using Application.Features.Products.Queries.GetProductsByPrice;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Validators
{
    public class GetProductsByPriceQueryValidator:AbstractValidator<GetProductsByPriceQuery>
    {

        public GetProductsByPriceQueryValidator()
        {
            RuleFor(x => x.Min)
                .NotEmpty()
                .WithMessage("Minimum Değer Boş Bırakılamaz")
                .GreaterThan(0)
                .WithMessage("Minimum Değer 1'den Küçük Olamaz")
                .LessThan(x => x.Max)
                .WithMessage("Minimum Değer Maksimum Değerden Büyük veya Eşit Olamaz");

            RuleFor(x => x.Max)
               .NotEmpty()
               .WithMessage("Maksimum Değer Boş Bırakılamaz")
               .GreaterThan(0)
               .WithMessage("Maksimum Değer 1'den Küçük Olamaz")
               .GreaterThan(x => x.Min)
               .WithMessage("Maksimum Değer Minimum Değerden Küçük veya Eşit Olamaz");


           RuleFor(x => x)
          .Must(x => x.Max != x.Min)
          .WithMessage("Maksimum ve minimum değerler eşit olmamalıdır.");

            RuleFor(x => x.Page)
                .NotEmpty()
                .WithMessage("Sayfa Bilgisi Boş Bırakılamaz")
                .GreaterThan(0)
                .WithMessage("Sayfa Numara Değeri Sıfırdan Büyük Olmalıdır");

            RuleFor(x => x.PageSize)
                 .NotEmpty()
                 .WithMessage("Sayfa Kayıt Bilgisi Boş Bırakılamaz")
                 .GreaterThan(0)
                 .WithMessage("Sayfa Kayıt Numara Değeri Sıfırdan Büyük Olmalıdır");

        }


    }
}
