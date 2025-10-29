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
            RuleFor(x => x.SecretKey)
                .NotEmpty().WithMessage("Gizli Şifre Boş Olamaz")
                .MinimumLength(7).WithMessage("Gizli Şifre toplam 7 karakter girilmeli")
                .MaximumLength(7).WithMessage("Gizli Şifre toplam 7 karakter girilmeli");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-Posta Adresi Boş Olamaz")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
            RuleFor(x => x.Fullname)
                .NotEmpty().WithMessage("Adınız Soyadınız Boş Olamaz");
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz");
            RuleFor(x => x.RPassword)
                .NotEmpty().WithMessage("Şifre Tekrarı Boş Olamaz");
        }
    }
}
