using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.Families.Requests;

public record CreateFamilyRequest(string Name) : IFamilyRequest, IRequest<Result<FamilyResponse>>;
