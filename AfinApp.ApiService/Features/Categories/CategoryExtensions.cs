using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Categories;

public static class CategoryExtensions
{
    public static CategoryResponse ConvertToResponse(this Category i) => new(i.Id, i.Name, i.Type, i.HexColor);
}
