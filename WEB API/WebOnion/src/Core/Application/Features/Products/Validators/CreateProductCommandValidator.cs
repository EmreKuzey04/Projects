using Application.Features.Products.Commands.CreateProduct;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Validators
{
    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x=>x.ProductName)
                .NotEmpty()
                .WithMessage("Ürün Adı Boş Bırakılamaz")
                .MinimumLength(2)
                .WithMessage("Ürün Adı en az 2 karakter olmalıdır");

            RuleFor(x => x.UnitPrice)
                .NotEmpty()
                .WithMessage("Ürün Fiyatı Boş Bırakılamaz")
                .GreaterThan(0)
                .WithMessage("Ürün Fiyatı 0'dan Büyük Olmalıdır");

            RuleFor(x => x.UnitsInStock)
                .NotEmpty()
                .WithMessage("Ürün Stoğu Boş Bırakılamaz")
                .GreaterThan((short)0)
                .WithMessage("Stok 0' dan Büyük Olmalıdır");


            //RuleFor(x => x.CategoryId)
            //    .NotEmpty()
            //    .WithMessage("Ürün Kategori Id'si Boş Bırakılamaz")
            //    .GreaterThan(0)
            //    .WithMessage("Kategori Id 0'dan Büyük Olmalıdır");


             RuleFor(x => x.SupplierId)
            .NotEmpty()
            .WithMessage("Tedarikçi Boş Bırakılamaz")
            .GreaterThan(0)
            .WithMessage("Tedarikçi Id Değeri Sıfırdan Büyük Olmalıdır");
        } 
    }
}
