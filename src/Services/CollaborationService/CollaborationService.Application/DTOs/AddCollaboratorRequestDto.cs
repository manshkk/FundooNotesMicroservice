namespace CollaborationService.Application.DTOs;

public class AddCollaboratorRequestDto
{
    public int NoteId { get; set; }

    public string CollaboratorEmail { get; set; } = string.Empty;
}