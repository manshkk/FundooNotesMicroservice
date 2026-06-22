using MediatR;
using NotesService.Application.DTOs;

namespace NotesService.Application.Commands.CreateNote;

public record CreateNoteCommand(
    CreateNoteDto Dto,
    int UserId) : IRequest<int>;