using MediatR;
using NotesService.Application.DTOs;

namespace NotesService.Application.Commands.UpdateNote;

public record UpdateNoteCommand(
    int Id,
    UpdateNoteDto Dto,
    int UserId)
    : IRequest<bool>;