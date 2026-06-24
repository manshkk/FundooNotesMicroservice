using CollaborationService.Application.Interfaces;
using CollaborationService.Domain.Entities;
using MediatR;

namespace CollaborationService.Application.Queries.GetCollaborators;

public class GetCollaboratorsQueryHandler
    : IRequestHandler<GetCollaboratorsQuery, List<Collaborator>>
{
    private readonly ICollaboratorRepository _repository;

    public GetCollaboratorsQueryHandler(
        ICollaboratorRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Collaborator>> Handle(
        GetCollaboratorsQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository
            .GetCollaboratorsByNoteIdAsync(request.NoteId);
    }
}