using CollaborationService.Application.Interfaces;
using CollaborationService.Domain.Entities;
using MediatR;

namespace CollaborationService.Application.Commands.AddCollaborator;

public class AddCollaboratorCommandHandler
    : IRequestHandler<AddCollaboratorCommand, bool>
{
    private readonly IUserServiceClient _userServiceClient;
    private readonly ICollaboratorRepository _collaboratorRepository;
    private readonly INotesServiceClient _notesServiceClient;

    public AddCollaboratorCommandHandler(
    IUserServiceClient userServiceClient,
    INotesServiceClient notesServiceClient,
    ICollaboratorRepository collaboratorRepository)
    {
        _userServiceClient = userServiceClient;
        _notesServiceClient = notesServiceClient;
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
        var note =
    await _notesServiceClient
        .GetNoteByIdAsync(
            request.Request.NoteId,
            request.Token);

        if (note == null)
        {
            return false;
        }

        if (note.UserId != request.OwnerUserId)
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