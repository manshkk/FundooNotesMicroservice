using System.Net.Http.Headers;
using System.Net.Http.Json;
using CollaborationService.Application.DTOs;
using CollaborationService.Application.Interfaces;

namespace CollaborationService.Infrastructure.Services;

public class NotesServiceClient : INotesServiceClient
{
    private readonly HttpClient _httpClient;

    public NotesServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<NoteDto?> GetNoteByIdAsync(
        int noteId,
        string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token);

        var response =
            await _httpClient.GetAsync(
                $"api/notes/{noteId}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content
            .ReadFromJsonAsync<NoteDto>();
    }
}