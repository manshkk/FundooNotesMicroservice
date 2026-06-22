using MediatR;

namespace NotesService.Application.Commands.UnarchiveNote;

public record UnarchiveNoteCommand(int Id) : IRequest<bool>;