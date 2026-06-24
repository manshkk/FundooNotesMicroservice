namespace CollaborationService.Application.DTOs;

public class AddCollaboratorRequestDto
{
    public int NoteId { get; set; }

    public int OwnerUserId { get; set; }

    public string CollaboratorEmail { get; set; } = string.Empty;
}