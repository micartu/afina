using AfinApp.Data.Entities;

namespace AfinApp.ApiService.Features.Holdings;

public static class HoldingExtensions
{
    public static HoldingResponse ConvertToResponse(this Holding i) => new(
            i.Id,
            i.AccountId,
            i.AssetId,
            i.Quantity,
            i.AverageCostBasis
            );
}