using CollaborationService.Domain.Entities;
using MediatR;

namespace CollaborationService.Application.Queries.GetCollaborators;

public record GetCollaboratorsQuery(int NoteId)
    : IRequest<List<Collaborator>>;