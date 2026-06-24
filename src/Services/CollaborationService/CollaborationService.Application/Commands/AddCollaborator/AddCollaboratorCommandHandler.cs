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
        var user = await _userServiceClient
            .GetUserByEmailAsync(
                request.Request.CollaboratorEmail);

        if (user == null)
        {
            return false;
        }

        var collaborator = new Collaborator
        {
            NoteId = request.Request.NoteId,
            OwnerUserId = request.OwnerUserId,
            CollaboratorUserId = user.UserId,
            CollaboratorEmail = user.Email,
            CreatedAt = DateTime.UtcNow
        };

        await _collaboratorRepository
            .AddCollaboratorAsync(collaborator);

        return true;
    }
}