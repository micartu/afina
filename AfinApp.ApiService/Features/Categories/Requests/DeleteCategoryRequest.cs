using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.Categories.Requests;

public record DeleteCategoryRequest(Guid Id) : IRequest<Result>;
