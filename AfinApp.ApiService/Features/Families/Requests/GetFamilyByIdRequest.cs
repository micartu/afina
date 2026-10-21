using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.Families.Requests;

public record GetFamilyByIdRequest(Guid Id) : IRequest<Result<FamilyResponse>>;
