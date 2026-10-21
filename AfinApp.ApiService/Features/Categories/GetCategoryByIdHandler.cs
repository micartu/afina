using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using AfinApp.ApiService.Features.Categories.Requests;

namespace AfinApp.ApiService.Features.Categories;

public class GetCategoryByIdHandler(ICategoryRelevantContext ctx) : IRequestHandler<GetCategoryByIdRequest, Result<CategoryResponse>>
{
    public async ValueTask<Result<CategoryResponse>> Handle(GetCategoryByIdRequest req, CancellationToken ct)
    {
        var item = await ctx.Categories
            .Where(x => x.Id == req.Id)
            .Select(x => x.ConvertToResponse())
            .FirstOrDefaultAsync(ct);
        return item != null
            ? item
            : new Error(ErrorType.NotFound, $"{Const.apiCategories}.NotFound", "Not found");
    }
}