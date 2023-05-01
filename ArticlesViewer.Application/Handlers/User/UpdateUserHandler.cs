using ArticlesViewer.Application.Commands;
using ArticlesViewer.Application.RepositoryContracts;
using MediatR;

namespace ArticlesViewer.Application.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserHandler(IPublisher publisher, IUnitOfWork unitOfWork)
    {
        _publisher = publisher;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _publisher.Publish(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
    }
}
