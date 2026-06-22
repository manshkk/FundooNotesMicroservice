using MediatR;
using NotesService.Application.DTOs;

namespace NotesService.Application.Queries.GetAllNotes;

public record GetAllNotesQuery()
    : IRequest<List<NoteResponseDto>>;