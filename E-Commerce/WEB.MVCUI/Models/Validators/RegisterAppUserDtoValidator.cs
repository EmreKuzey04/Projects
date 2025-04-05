using FluentValidation;
using WEB.MVCUI.Models.Dtos;

namespace WEB.MVCUI.Models.Validators
{
    public class RegisterAppUserDtoValidator:AbstractValidator<RegisterAppUserDto>
    {

        public RegisterAppUserDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Ad Soyad Boş Bırakılamaz")
                .MinimumLength(3)
                .WithMessage("Ad Soyad En Az 3 Karakterden Oluşmalıdır")
                .MaximumLength(20)
                .WithMessage("Ad Soyad En Fazla 20 Karakterden Oluşmalıdır");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email Boş Bırakılamaz")
                .EmailAddress()
                .WithMessage("Lütfen Email Formatına(@) Uygun Şekilde Giriniz");

            RuleFor(x => x.Password)
               .NotEmpty()
               .WithMessage("Şifre Boş Bırakılamaz")
               .Must(ContainsUpperLetter)
               .WithMessage("Şifre İçinde En Az Bir Büyük Harf Olmalıdır")
               .MinimumLength(8)
               .WithMessage("Şifre En Az 8 Karakterden Oluşmalıdır");


            RuleFor(x => x.RePassword)
              .NotEmpty()
              .WithMessage("Şifre Tekrarı Boş Bırakılamaz")
              .Equal(x => x.Password)
              .WithMessage("Şifre İle Şifre Tekrarı Aynı Olmalıdır")
              .Must(ContainsUpperLetter)
              .WithMessage("Şifre Tekrarı İçinde En Az Bir Büyük Harf Olmalıdır");

            RuleFor(x => x.CityId)
                .GreaterThan(0)
                .WithMessage("Şehir Boş Bırakılamaz");

            RuleFor(x => x.DistrictId)
                .GreaterThan(0)
                .WithMessage("İlçe Boş Bırakılamaz");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Adres Boş Bırakılamaz");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Telefon Numarası Boş Bırakılamaz");



        }

        private bool ContainsUpperLetter(string password)
        {
            if (password != null)
            {
                for (int i = 0; i < password.Length; i++)
                {

                    if (char.IsUpper(password[i]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
