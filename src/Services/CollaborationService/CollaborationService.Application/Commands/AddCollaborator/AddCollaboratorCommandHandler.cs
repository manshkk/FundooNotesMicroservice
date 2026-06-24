using CollaborationService.Application.Interfaces;
using CollaborationService.Domain.Entities;
using MediatR;

namespace CollaborationService.Application.Commands.AddCollaborator;

public class AddCollaboratorCommandHandler
    : IRequestHandler<AddCollaboratorCommand, bool>
{
    private readonly IUserServiceClient _userServiceClient;
    private readonly ICollaboratorRepository _collaboratorRepository;

    public AddCollaboratorCommandHandler(
        IUserServiceClient userServiceClient,
        ICollaboratorRepository collaboratorRepository)
    {
        _userServiceClient = userServiceClient;
        _collaboratorRepository = collaboratorRepository;
    }

    public async Task<bool> Handle(
        AddCollaboratorCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}