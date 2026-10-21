using Carter;
using Mediator;
using AfinApp.Data.Shared;
using AfinApp.ApiService.Helpers;
using AfinApp.ApiService.Features.Families.Requests;
using FluentValidation;
using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Families;

public static class FamilyExtensions
{
    public static FamilyResponse ConvertToResponse(this Family i) => new(i.Id, i.Name);
}

public record FamilyResponse(Guid Id, string Name);

public class FamilyValidator : AbstractValidator<IFamilyRequest>
{
    public FamilyValidator() { RuleFor(x => x.Name).NotEmpty(); }
}

public class FamilyModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var end = app.MapGroup(Const.apiFamilies).RequireAuthorization();
        end.MapGet("", async (IMediator m) => Results.Ok(await m.Send(new GetAllFamiliesRequest())));
        end.MapGet(
                "{id}", async (Guid id, IMediator m) =>
                {
                    var r = await m.Send(new GetFamilyByIdRequest(id));
                    return r.IsSuccess
                    ? Results.Ok(r.Value)
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
        end.MapPost(
                "", async (CreateFamilyRequest req, IMediator m) =>
                {
                    var r = await m.Send(req);
                    return r.IsSuccess
                    ? Results.Created($"{Const.apiFamilies}/{r.Value!.Id}", r.Value)
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
        end.MapPut(
                "{id}",
                async (Guid id, UpdateFamilyRequest req, IMediator m) =>
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
                    var r = await m.Send(new DeleteFamilyRequest(id));
                    return r.IsSuccess
                    ? Results.NoContent()
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
    }
}