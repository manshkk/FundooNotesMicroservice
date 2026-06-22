namespace NotesService.Domain.Entities;

public class Note
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public int UserId { get; set; }

    public bool IsPinned { get; set; }

    public bool IsArchived { get; set; }

    public bool IsDeleted { get; set; }

    public string Color { get; set; } = "#FFFFFF";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}