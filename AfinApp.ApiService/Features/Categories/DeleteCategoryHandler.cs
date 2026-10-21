using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using AfinApp.ApiService.Features.Categories.Requests;

namespace AfinApp.ApiService.Features.Categories;

public class DeleteCategoryHandler(ICategoryRelevantContext ctx) : IRequestHandler<DeleteCategoryRequest, Result>
{
    public async ValueTask<Result> Handle(DeleteCategoryRequest req, CancellationToken ct)
    {
        var item = await ctx.Categories.FindAsync([req.Id], ct);
        if (item == null)
            return new Error(ErrorType.NotFound, $"{Const.apiCategories}.NotFound", "Not found");
        ctx.Categories.Remove(item);
        await ctx.SaveChangesAsync(ct);
        return Result.Success();
    }
}