using ArticlesViewer.Application.Commands;
using ArticlesViewer.Application.DTO;
using ArticlesViewer.Application.RepositoryContracts;
using AutoMapper;
using MediatR;

namespace ArticlesViewer.Application.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserResponse>
{
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserHandler(IPublisher publisher, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _publisher = publisher;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _publisher.Publish(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<UserResponse>(request);
    }
}
