namespace NotesService.Application.DTOs;

public class NoteResponseDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public int UserId { get; set; }

    public bool IsPinned { get; set; }

    public bool IsArchived { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }
}