using Mediator;
using Microsoft.EntityFrameworkCore;
using AfinApp.ApiService.Features.Holdings.Requests;
using AfinApp.Data.Context;

namespace AfinApp.ApiService.Features.Holdings;

public class GetAllHoldingsHandler(IHoldingRelevantContext ctx) : IRequestHandler<GetAllHoldingsRequest, IEnumerable<HoldingResponse>>
{
    public async ValueTask<IEnumerable<HoldingResponse>> Handle(GetAllHoldingsRequest req, CancellationToken ct)
        => await ctx.Holdings.Select(x => x.ConvertToResponse()).ToListAsync(ct);
}
