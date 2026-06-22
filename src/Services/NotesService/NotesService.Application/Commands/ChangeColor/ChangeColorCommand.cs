using MediatR;
using NotesService.Application.DTOs;

namespace NotesService.Application.Commands.ChangeColor;

public record ChangeColorCommand(
    int Id,
    ChangeColorDto Dto)
    : IRequest<bool>;