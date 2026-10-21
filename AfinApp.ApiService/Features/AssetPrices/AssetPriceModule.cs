using Carter;
using Mediator;
using AfinApp.Data.Shared;
using AfinApp.ApiService.Helpers;
using AfinApp.ApiService.Features.AssetPrices.Requests;

namespace AfinApp.ApiService.Features.AssetPrices;

public class AssetPriceModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var end = app.MapGroup(Const.apiAssetPrices).RequireAuthorization();
        end.MapGet(
                "",
                async (IMediator m) =>
                Results.Ok(await m.Send(new GetAllAssetPricesRequest()))
                );
        end.MapGet(
                "{id}",
                async (Guid id, IMediator m) =>
                {
                    var r = await m.Send(new GetAssetPriceByIdRequest(id));
                    return r.IsSuccess
                    ? Results.Ok(r.Value)
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
        end.MapPost(
                "",
                async (CreateAssetPriceRequest req, IMediator m) =>
                {
                    var r = await m.Send(req);
                    return r.IsSuccess
                    ? Results.Created($"{Const.apiAssetPrices}/{r.Value!.Id}", r.Value)
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
        end.MapPut(
                "{id}",
                async (Guid id, UpdateAssetPriceRequest req, IMediator m) =>
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
                    var r = await m.Send(new DeleteAssetPriceRequest(id));
                    return r.IsSuccess
                    ? Results.NoContent()
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
    }
}