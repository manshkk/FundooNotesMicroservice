using MediatR;

namespace NotesService.Application.Commands.PinNote;

public record PinNoteCommand(
    int Id,
    int UserId) : IRequest<bool>;