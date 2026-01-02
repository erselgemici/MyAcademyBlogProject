using Blogy.Business.DTOs.ContactDtos;
using FluentValidation;

namespace Blogy.Business.Validators.ContactValidators
{
    public class SendMessageValidator : AbstractValidator<SendMessageDto>
    {
        public SendMessageValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş geçilemez.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail alanı boş geçilemez.")
                                 .EmailAddress().WithMessage("Geçerli bir mail adresi giriniz.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu boş geçilemez.");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Mesaj boş geçilemez.")
                                   .MinimumLength(10).WithMessage("Mesaj en az 10 karakter olmalıdır.");
        }
    }
}
