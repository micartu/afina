using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using FluentValidation;
using AfinApp.ApiService.Features.Holdings.Requests;

namespace AfinApp.ApiService.Features.Holdings;

public class UpdateHoldingHandler(IHoldingRelevantContext ctx, IValidator<IHoldingRequest> val) : IRequestHandler<UpdateHoldingRequest, Result<HoldingResponse>>
{
    public async ValueTask<Result<HoldingResponse>> Handle(UpdateHoldingRequest req, CancellationToken ct)
    {
        if (!val.Validate(req).IsValid)
            return new Error(ErrorType.Validation, Const.apiHoldings, "Validation failed");
        var item = await ctx.Holdings.FindAsync([req.Id], ct);
        if (item == null)
            return new Error(ErrorType.NotFound, $"{Const.apiHoldings}.NotFound", "Not found");
        item.AccountId = req.AccountId;
        item.AssetId = req.AssetId;
        item.Quantity = req.Quantity;
        item.AverageCostBasis = req.AverageCostBasis;
        await ctx.SaveChangesAsync(ct);
        return item.ConvertToResponse();
    }
}