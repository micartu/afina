using Mediator;
using AfinApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using AfinApp.ApiService.Features.Categories.Requests;

namespace AfinApp.ApiService.Features.Categories;

public class GetAllCategoriesHandler(ICategoryRelevantContext ctx) : IRequestHandler<GetAllCategoriesRequest, IEnumerable<CategoryResponse>>
{
    public async ValueTask<IEnumerable<CategoryResponse>> Handle(GetAllCategoriesRequest req, CancellationToken ct)
        => await ctx.Categories.Select(x => x.ConvertToResponse()).ToListAsync(ct);
}
