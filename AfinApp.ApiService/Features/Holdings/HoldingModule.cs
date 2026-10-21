using Carter;
using Mediator;
using AfinApp.Data.Shared;
using AfinApp.ApiService.Helpers;
using AfinApp.ApiService.Features.Holdings.Requests;

namespace AfinApp.ApiService.Features.Holdings;

public class HoldingModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var end = app.MapGroup(Const.apiHoldings).RequireAuthorization();
        end.MapGet("", async (IMediator m) => Results.Ok(await m.Send(new GetAllHoldingsRequest())));
        end.MapGet("{id}",
                async (Guid id, IMediator m) =>
                {
                    var r = await m.Send(new GetHoldingByIdRequest(id));
                    return r.IsSuccess
                    ? Results.Ok(r.Value)
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
        end.MapPost("",
                async (CreateHoldingRequest req, IMediator m) =>
                {
                    var r = await m.Send(req);
                    return r.IsSuccess
                    ? Results.Created($"{Const.apiHoldings}/{r.Value!.Id}", r.Value)
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
        end.MapPut("{id}",
                async (Guid id, UpdateHoldingRequest req, IMediator m) =>
                {
                    var r = await m.Send(req with { Id = id });
                    return r.IsSuccess
                    ? Results.Ok(r.Value)
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
        end.MapDelete("{id}",
                async (Guid id, IMediator m) =>
                {
                    var r = await m.Send(new DeleteHoldingRequest(id));
                    return r.IsSuccess
                    ? Results.NoContent()
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
    }
}