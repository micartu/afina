using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Categories.Requests;

public interface ICategoryRequest { string Name { get; } TransactionType Type { get; } string HexColor { get; } }