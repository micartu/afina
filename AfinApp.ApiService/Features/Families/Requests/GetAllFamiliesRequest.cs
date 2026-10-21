using Mediator;

namespace AfinApp.ApiService.Features.Families.Requests;

public record GetAllFamiliesRequest() : IRequest<IEnumerable<FamilyResponse>>;
