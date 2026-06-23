using MediatR;
using NotesService.Application.DTOs;

namespace NotesService.Application.Queries.GetAllNotes;

public record GetAllNotesQuery(int UserId)
    : IRequest<List<NoteResponseDto>>;