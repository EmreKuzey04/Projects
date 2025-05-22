using FluentValidation;
using WEB.MVCUI.Areas.Admin.Models.Dtos;

namespace WEB.MVCUI.Areas.Admin.Models.Validators
{
    public class ProfileUpdatePasswordDtoValidator:AbstractValidator<ProfileUpdatePasswordDto>
    {

        public ProfileUpdatePasswordDtoValidator() 
        {
            RuleFor(x => x.Password)
                  .NotEmpty()
                  .WithMessage("Şifre Boş Bırakılamaz");

            RuleFor(x=>x.NewPassword)
                .NotEmpty()
                .WithMessage("Yeni Şifre Boş Bırakılamaz")
                .Must(ContainsUpperLetter)
                .WithMessage("Yeni Şifre İçinde En Az Bir Büyük Harf Olmalıdır")
                .MinimumLength(8)
                .WithMessage("Yeni Şifre En Az 8 Karakterden Oluşmalıdır");

            RuleFor(x=>x.ReNewPassword)
                .NotEmpty()
                .WithMessage("Yeni Şifre Tekrarı Boş Bırakılamaz")
                .Must(ContainsUpperLetter)
                .WithMessage("Yeni Şifre Tekrarı İçinde En Az Bir Büyük Harf Olmalıdır")
                .Equal(x=>x.NewPassword)
                .WithMessage("Yeni Şifre İle Yeni Şifre Tekrarı Aynı Olmalıdır")
                .Must(ContainsUpperLetter)
                .WithMessage("Yeni Şifre Tekrarı İçinde En Az Bir Büyük Harf Olmalıdır");



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
