using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using FluentValidation;
using AfinApp.Data.Entities;
using AfinApp.ApiService.Features.Categories.Requests;

namespace AfinApp.ApiService.Features.Categories;

public class CreateCategoryHandler(ICategoryRelevantContext ctx, IValidator<ICategoryRequest> val) : IRequestHandler<CreateCategoryRequest, Result<CategoryResponse>>
{
    public async ValueTask<Result<CategoryResponse>> Handle(CreateCategoryRequest req, CancellationToken ct)
    {
        if (!val.Validate(req).IsValid)
            return new Error(ErrorType.Validation, Const.apiCategories, "Validation failed");
        var item = new Category
        {
            Id = Guid.NewGuid(),
            Name = req.Name,
            Type = req.Type,
            HexColor = req.HexColor
        };
        ctx.Categories.Add(item);
        await ctx.SaveChangesAsync(ct);
        return item.ConvertToResponse();
    }
}