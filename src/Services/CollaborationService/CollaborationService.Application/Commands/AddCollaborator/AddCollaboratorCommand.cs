using CollaborationService.Application.DTOs;
using MediatR;

namespace CollaborationService.Application.Commands.AddCollaborator;

public record AddCollaboratorCommand(
    AddCollaboratorRequestDto Request,
    int OwnerUserId
) : IRequest<bool>;