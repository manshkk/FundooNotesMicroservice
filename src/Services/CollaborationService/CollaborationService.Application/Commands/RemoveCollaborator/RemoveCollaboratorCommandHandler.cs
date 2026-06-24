using CollaborationService.Application.Interfaces;
using MediatR;

namespace CollaborationService.Application.Commands.RemoveCollaborator;

public class RemoveCollaboratorCommandHandler
    : IRequestHandler<RemoveCollaboratorCommand, bool>
{
    private readonly ICollaboratorRepository _repository;

    public RemoveCollaboratorCommandHandler(
        ICollaboratorRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        RemoveCollaboratorCommand request,
        CancellationToken cancellationToken)
    {
        return await _repository
            .RemoveCollaboratorAsync(
                request.CollaboratorId);
    }
}