using MediatR;

namespace NotesService.Application.Commands.UnpinNote;

public record UnpinNoteCommand(int Id) : IRequest<bool>;