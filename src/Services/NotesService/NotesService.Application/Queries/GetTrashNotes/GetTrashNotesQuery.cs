using MediatR;
using NotesService.Application.DTOs;

namespace NotesService.Application.Queries.GetTrashNotes;

public record GetTrashNotesQuery(
    int UserId)
    : IRequest<List<NoteResponseDto>>;