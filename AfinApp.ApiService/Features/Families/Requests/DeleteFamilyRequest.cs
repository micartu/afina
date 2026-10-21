using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.Families.Requests;

public record DeleteFamilyRequest(Guid Id) : IRequest<Result>;