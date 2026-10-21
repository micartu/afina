using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using FluentValidation;
using AfinApp.ApiService.Features.Categories.Requests;

namespace AfinApp.ApiService.Features.Categories;

public class UpdateCategoryHandler(ICategoryRelevantContext ctx, IValidator<ICategoryRequest> val) : IRequestHandler<UpdateCategoryRequest, Result<CategoryResponse>>
{
    public async ValueTask<Result<CategoryResponse>> Handle(UpdateCategoryRequest req, CancellationToken ct)
    {
        if (!val.Validate(req).IsValid)
            return new Error(ErrorType.Validation, Const.apiCategories, "Validation failed");
        var item = await ctx.Categories.FindAsync([req.Id], ct);
        if (item == null)
            return new Error(ErrorType.NotFound, $"{Const.apiCategories}.NotFound", "Not found");
        item.Name = req.Name;
        item.Type = req.Type;
        item.HexColor = req.HexColor;
        await ctx.SaveChangesAsync(ct);
        return item.ConvertToResponse();
    }
}