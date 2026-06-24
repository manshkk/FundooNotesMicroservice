using MediatR;

namespace CollaborationService.Application.Commands.AddCollaborator;

public class AddCollaboratorCommandHandler
    : IRequestHandler<AddCollaboratorCommand, bool>
{
    public Task<bool> Handle(
        AddCollaboratorCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}