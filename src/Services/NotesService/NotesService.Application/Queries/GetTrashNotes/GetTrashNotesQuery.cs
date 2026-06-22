using MediatR;
using NotesService.Application.DTOs;

namespace NotesService.Application.Queries.GetTrashNotes;

public record GetTrashNotesQuery()
    : IRequest<List<NoteResponseDto>>;