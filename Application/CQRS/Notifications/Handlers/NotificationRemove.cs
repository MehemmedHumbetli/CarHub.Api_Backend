using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Notifications.Handlers;

public class NotificationRemove
{
    public class NotificationDeleteCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<NotificationDeleteCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(NotificationDeleteCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.NotificationRepository.Remove(request.Id);
            await _unitOfWork.SaveChangeAsync();
            return new Result<Unit> { Errors = [], IsSuccess = true };
        }
    }
}
