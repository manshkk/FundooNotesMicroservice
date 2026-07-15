namespace SharedLibrary.Caching.Constants;

public static class CacheKeys
{
    public static string UserNotes(int userId)
        => $"notes:user:{userId}";

    public static string TrashNotes(int userId)
        => $"notes:trash:user:{userId}";

    public static string NoteById(int userId, int noteId)
    => $"note:user:{userId}:{noteId}";
}