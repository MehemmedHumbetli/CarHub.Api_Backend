using Application.Services;
using MediatR;
using Repository.Common;

namespace Application.CQRS.EmailVertification.Handlers;

public class EmailVerificationHandler
{
    public class GenerateEmailCodeCommand : IRequest
    {
        public string Email { get; set; }
    }

    public class GenerateEmailCodeCommandHandler : IRequestHandler<GenerateEmailCodeCommand>
    {
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public GenerateEmailCodeCommandHandler(IEmailService emailService, IUnitOfWork unitOfWork)
        {
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(GenerateEmailCodeCommand request, CancellationToken cancellationToken)
        {
            var code = new Random().Next(100000, 999999).ToString();

            var verification = new Domain.Entities.EmailVerification
            {
                Email = request.Email,
                Code = code,
                ExpireAt = DateTime.UtcNow.AddMinutes(5),
                CreatedDate = DateTime.UtcNow
            };

            await _unitOfWork.EmailVerificationRepository.AddAsync(verification);
            await _unitOfWork.SaveChangeAsync();

            string body = $@"
<div style='font-family: Arial, sans-serif; max-width: 500px; margin: auto; padding: 24px; border: 1px solid #e0e0e0; border-radius: 8px; background-color: #ffffff; color: #333333;'>
  <div style='text-align: center; margin-bottom: 24px;'>
    <h1 style='font-size: 28px; font-weight: bold; margin: 0;'>
      <span style='color: #2196F3;'>Car</span><span style='color: #333;'>Hub</span>
    </h1>
  </div>
  <h2 style='font-size: 20px; font-weight: normal; color: #4CAF50; text-align: center; margin-bottom: 16px;'>Email Təsdiqi</h2>
  <p style='font-size: 16px; line-height: 1.5; margin-bottom: 16px;'>Salam! Qeydiyyatı tamamlamaq üçün aşağıdakı təsdiq kodunu istifadə edin:</p>
  <div style='font-size: 24px; font-weight: bold; letter-spacing: 2px; color: #2196F3; background-color: #f5f5f5; padding: 12px; text-align: center; border-radius: 6px; margin-bottom: 16px;'>
    {code}
  </div>
  <p style='font-size: 14px; color: #888888; margin-bottom: 24px;'>Bu kod <strong>5 dəqiqə</strong> ərzində etibarlıdır.</p>
  <hr style='border: none; border-top: 1px solid #e0e0e0; margin: 24px 0;' />
  <p style='font-size: 12px; color: #999999; text-align: center;'>Bu mesaj <strong>CarHub</strong> tərəfindən göndərilmişdir.</p>
</div>";



            await _emailService.SendEmailAsync(request.Email, "Təsdiq Kodu", body);

            return Unit.Value;
        }
    }

}
