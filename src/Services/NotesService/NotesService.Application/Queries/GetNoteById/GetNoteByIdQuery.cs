using MediatR;
using NotesService.Application.DTOs;

namespace NotesService.Application.Queries.GetNoteById;

public record GetNoteByIdQuery(int Id)
    : IRequest<NoteResponseDto?>;