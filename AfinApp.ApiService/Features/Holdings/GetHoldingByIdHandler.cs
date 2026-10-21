using Mediator;
using AfinApp.Data.Shared;
using Microsoft.EntityFrameworkCore;
using AfinApp.Data.Context;
using FluentValidation;
using AfinApp.ApiService.Features.Holdings.Requests;

namespace AfinApp.ApiService.Features.Holdings;

public class GetHoldingByIdHandler(IHoldingRelevantContext ctx) : IRequestHandler<GetHoldingByIdRequest, Result<HoldingResponse>>
{
    public async ValueTask<Result<HoldingResponse>> Handle(GetHoldingByIdRequest req, CancellationToken ct)
    {
        var item = await ctx.Holdings
            .Where(x => x.Id == req.Id)
            .Select(x => x.ConvertToResponse())
            .FirstOrDefaultAsync(ct);
        return item != null
            ? item
            : new Error(ErrorType.NotFound, $"{Const.apiHoldings}.NotFound", "Not found");
    }
}