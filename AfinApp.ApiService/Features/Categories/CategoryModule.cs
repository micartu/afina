using Carter;
using Mediator;
using AfinApp.Data.Shared;
using AfinApp.ApiService.Helpers;
using AfinApp.ApiService.Features.Categories.Requests;
using FluentValidation;

namespace AfinApp.ApiService.Features.Categories;

public class CategoryValidator : AbstractValidator<ICategoryRequest>
{
    public CategoryValidator() { RuleFor(x => x.Name).NotEmpty(); RuleFor(x => x.HexColor).NotEmpty(); }
}

public class CategoryModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var end = app.MapGroup(Const.apiCategories).RequireAuthorization();
        end.MapGet("", async (IMediator m) => Results.Ok(await m.Send(new GetAllCategoriesRequest())));
        end.MapGet(
                "{id}",
                async (Guid id, IMediator m) =>
                {
                    var r = await m.Send(new GetCategoryByIdRequest(id));
                    return r.IsSuccess
                    ? Results.Ok(r.Value)
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
        end.MapPost(
                "",
                async (CreateCategoryRequest req, IMediator m) =>
                {
                    var r = await m.Send(req);
                    return r.IsSuccess
                    ? Results.Created($"{Const.apiCategories}/{r.Value!.Id}", r.Value)
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
        end.MapPut(
                "{id}",
                async (Guid id, UpdateCategoryRequest req, IMediator m) =>
                {
                    var r = await m.Send(req with { Id = id });
                    return r.IsSuccess
                    ? Results.Ok(r.Value)
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
        end.MapDelete(
                "{id}",
                async (Guid id, IMediator m) =>
                {
                    var r = await m.Send(new DeleteCategoryRequest(id));
                    return r.IsSuccess
                    ? Results.NoContent()
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
    }
}