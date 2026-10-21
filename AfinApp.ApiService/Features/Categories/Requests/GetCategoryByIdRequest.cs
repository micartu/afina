using Mediator;
using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Features.Categories.Requests;

public record GetCategoryByIdRequest(Guid Id) : IRequest<Result<CategoryResponse>>;
