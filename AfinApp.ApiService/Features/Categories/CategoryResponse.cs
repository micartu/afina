using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Categories;

public record CategoryResponse(Guid Id, string Name, TransactionType Type, string HexColor);
