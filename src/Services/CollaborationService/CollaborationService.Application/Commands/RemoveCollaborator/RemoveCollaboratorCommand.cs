using MediatR;

namespace CollaborationService.Application.Commands.RemoveCollaborator;

public record RemoveCollaboratorCommand(
    int CollaboratorId
) : IRequest<bool>;