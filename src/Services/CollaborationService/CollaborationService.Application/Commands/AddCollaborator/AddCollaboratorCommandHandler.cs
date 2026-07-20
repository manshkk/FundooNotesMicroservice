public async Task<bool> Handle(
    AddCollaboratorCommand request,
    CancellationToken cancellationToken)
{
    // Check Collaborator User
    var user = await _userServiceClient
        .GetUserByEmailAsync(request.Request.CollaboratorEmail);

    if (user == null)
    {
        return false;
    }

    // Check Note
    var note = await _notesServiceClient
        .GetNoteByIdAsync(
            request.Request.NoteId,
            request.Token);

    if (note == null)
    {
        return false;
    }

    // Check Owner
    if (note.UserId != request.OwnerUserId)
    {
        return false;
    }

    // Save Collaborator
    var collaborator = new Collaborator
    {
        NoteId = request.Request.NoteId,
        OwnerUserId = request.OwnerUserId,
        CollaboratorUserId = user.UserId,
        CollaboratorEmail = user.Email,
        CreatedAt = DateTime.UtcNow
    };

    await _collaboratorRepository.AddCollaboratorAsync(collaborator);

    return true;
}