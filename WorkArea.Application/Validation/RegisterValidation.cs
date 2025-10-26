using WorkArea.Application.RequestModels;
using FluentValidation;

namespace WorkArea.Application.Validation
{
    public class RegisterValidation : AbstractValidator<RegisterRequestModel>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre Boş Olamaz");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-Posta Adresi Boş Olamaz")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
            RuleFor(x => x.Firstname)
                .NotEmpty().WithMessage("Adınız Boş Olamaz");
            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyadınız Boş Olamaz");
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz");
            RuleFor(x => x.RPassword)
                .NotEmpty().WithMessage("Şifre Tekrarı Boş Olamaz");
        }
    }
}
