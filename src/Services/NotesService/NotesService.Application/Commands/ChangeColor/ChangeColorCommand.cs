using MediatR;
using NotesService.Application.DTOs;

namespace NotesService.Application.Commands.ChangeColor;

public record ChangeColorCommand(
    int Id,
    ChangeColorDto Dto,
    int UserId)
    : IRequest<bool>;