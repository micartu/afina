using Carter;
using Mediator;
using AfinApp.Data.Shared;
using AfinApp.ApiService.Helpers;
using AfinApp.ApiService.Features.Transactions.Requests;

namespace AfinApp.ApiService.Features.Transactions;

public class TransactionModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var end = app.MapGroup(Const.apiTransactions).RequireAuthorization();
        end.MapGet("",
                async (IMediator m) =>
                Results.Ok(await m.Send(new GetAllTransactionsRequest()))
                );
        end.MapGet("{id}",
                async (Guid id, IMediator m) =>
                {
                    var r = await m.Send(new GetTransactionByIdRequest(id));
                    return r.IsSuccess
                    ? Results.Ok(r.Value)
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
        end.MapPost("",
                async (CreateTransactionRequest req, IMediator m) =>
                {
                    var r = await m.Send(req);
                    return r.IsSuccess
                    ? Results.Created($"{Const.apiTransactions}/{r.Value!.Id}", r.Value)
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
        end.MapPut("{id}",
                async (Guid id, UpdateTransactionRequest req, IMediator m) =>
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
                    var r = await m.Send(new DeleteTransactionRequest(id));
                    return r.IsSuccess
                    ? Results.NoContent()
                    : ApiResultHelper.HandleError(r.Error, "Error");
                }
                );
    }
}