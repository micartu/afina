using Carter;
using Mediator;
using AfinApp.Data.Shared;
using AfinApp.ApiService.Helpers;
using AfinApp.ApiService.Features.Assets.Requests;

namespace AfinApp.ApiService.Features.Assets;

public class AssetModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var end = app.MapGroup(Const.apiAssets).RequireAuthorization();
        end.MapGet(
                "",
                async (IMediator m) => Results.Ok(await m.Send(new GetAllAssetsRequest()))
                );
        end.MapGet(
                "{id}",
                async (Guid id, IMediator m) =>
                {
                    var r = await m.Send(new GetAssetByIdRequest(id));
                    return r.IsSuccess
                    ? Results.Ok(r.Value)
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
        end.MapPost(
                "",
                async (CreateAssetRequest req, IMediator m) =>
                {
                    var r = await m.Send(req);
                    return r.IsSuccess
                    ? Results.Created($"{Const.apiAssets}/{r.Value!.Id}", r.Value)
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
        end.MapPut(
                "{id}",
                async (Guid id, UpdateAssetRequest req, IMediator m) =>
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
                    var r = await m.Send(new DeleteAssetRequest(id));
                    return r.IsSuccess
                    ? Results.NoContent()
                    : ApiResultHelper.HandleError(r.Error, "Error");
                });
    }
}