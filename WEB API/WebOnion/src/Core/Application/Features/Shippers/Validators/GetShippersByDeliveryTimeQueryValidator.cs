using Application.Features.Shippers.Queries.GetShippersByDeliveryTime;
using FluentValidation;

namespace Application.Features.Shippers.Validators
{
    public class GetShippersByDeliveryTimeQueryValidator : AbstractValidator<GetShippersByDeliveryTimeQuery>
    {
        public GetShippersByDeliveryTimeQueryValidator()
        {
            RuleFor(x => x.MinDays)
                .GreaterThan(0).WithMessage("Minimum teslimat günü 0'dan büyük olmalıdır.");

            RuleFor(x => x.MaxDays)
                .GreaterThan(x => x.MinDays)
                .WithMessage("Maksimum teslimat günü, minimum teslimat gününden büyük olmalıdır.");
        }
    }
}
