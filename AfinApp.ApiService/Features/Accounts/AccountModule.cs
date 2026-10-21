using Carter;
using Mediator;
using AfinApp.Data.Shared;
using AfinApp.ApiService.Helpers;
using AfinApp.ApiService.Features.Accounts.Requests;

namespace AfinApp.ApiService.Features.Accounts;

public class AccountModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var end = app.MapGroup(Const.apiAccounts).RequireAuthorization();
        end.MapGet(
                "",
                async (IMediator m) => Results.Ok(await m.Send(new GetAllAccountsRequest()))
                );
        end.MapGet(
                "{id}",
                async (Guid id, IMediator m) =>
                {
                    var r = await m.Send(new GetAccountByIdRequest(id));
                    return r.IsSuccess
                    ? Results.Ok(r.Value)
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
        end.MapPost(
                "",
                async (CreateAccountRequest req, IMediator m) =>
                {
                    var r = await m.Send(req);
                    return r.IsSuccess
                    ? Results.Created($"{Const.apiAccounts}/{r.Value!.Id}", r.Value)
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
        end.MapPut(
                "{id}",
                async (Guid id, UpdateAccountRequest req, IMediator m) =>
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
                    var r = await m.Send(new DeleteAccountRequest(id));
                    return r.IsSuccess
                    ? Results.NoContent()
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
    }
}