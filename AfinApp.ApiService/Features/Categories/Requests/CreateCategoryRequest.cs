using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Categories.Requests;

public record CreateCategoryRequest(string Name, TransactionType Type, string HexColor) : ICategoryRequest, IRequest<Result<CategoryResponse>>;
