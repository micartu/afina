using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.Families.Requests;

public record UpdateFamilyRequest(Guid Id, string Name) : IFamilyRequest, IRequest<Result<FamilyResponse>>;
