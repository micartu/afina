using Mediator;

namespace AfinApp.ApiService.Features.Categories.Requests;

public record GetAllCategoriesRequest() : IRequest<IEnumerable<CategoryResponse>>;
