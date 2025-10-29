using FluentValidation;
using WorkArea.Domain.Entities;

namespace WorkArea.Application.Validation
{
    public class ArchiveTypeValidation : AbstractValidator<ArchiveType>
    {
        public ArchiveTypeValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Arşiv Tipi Boş Olamaz");
        }
    }
}
