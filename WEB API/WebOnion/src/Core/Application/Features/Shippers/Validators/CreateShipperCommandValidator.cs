using Application.Features.Shippers.Commands.CreateShipper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shippers.Validators
{
    public class CreateShipperCommandValidator:AbstractValidator<CreateShipperCommand>
    {
        public CreateShipperCommandValidator() 
        {
            RuleFor(x => x.ShipperName)
                .NotEmpty()
                .WithMessage("Gönderici Firma İsmi Boş Bırakılamaz")
                .MinimumLength(3)
                .WithMessage("Gönderici Firma İsmi En Az 3 Karakterden Oluşmalıdır");

            RuleFor(x => x.ShipperId)
                .NotEmpty()
                .WithMessage("Gönderici Firma Id'si Boş Bırakılamaz");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Gönderici Firma İletişim Adresi Boş Bırakılamaz");
        }
    }
}
