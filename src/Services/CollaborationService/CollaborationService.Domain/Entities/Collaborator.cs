namespace CollaborationService.Domain.Entities;

public class Collaborator
{
    public int CollaboratorId { get; set; }

    public int NoteId { get; set; }

    public int OwnerUserId { get; set; }

    public int CollaboratorUserId { get; set; }

    public string CollaboratorEmail { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}