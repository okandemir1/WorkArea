using WorkArea.Application.RequestModels;
using FluentValidation;

namespace WorkArea.Application.Validation
{
    public class LoginValidation : AbstractValidator<LoginRequestModel>
    {
        public LoginValidation()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre Boş Olamaz");
        }
    }
}
