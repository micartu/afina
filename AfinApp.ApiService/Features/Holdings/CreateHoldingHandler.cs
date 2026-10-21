using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using FluentValidation;
using AfinApp.Data.Entities;
using AfinApp.ApiService.Features.Holdings.Requests;

namespace AfinApp.ApiService.Features.Holdings;

public class CreateHoldingHandler(IHoldingRelevantContext ctx, IValidator<IHoldingRequest> val) : IRequestHandler<CreateHoldingRequest, Result<HoldingResponse>>
{
    public async ValueTask<Result<HoldingResponse>> Handle(CreateHoldingRequest req, CancellationToken ct)
    {
        if (!val.Validate(req).IsValid)
            return new Error(ErrorType.Validation, Const.apiHoldings, "Validation failed");
        var item = new Holding
        {
            Id = Guid.NewGuid(),
            AccountId = req.AccountId,
            AssetId = req.AssetId,
            Quantity = req.Quantity,
            AverageCostBasis = req.AverageCostBasis
        };
        ctx.Holdings.Add(item);
        await ctx.SaveChangesAsync(ct);
        return item.ConvertToResponse();
    }
}