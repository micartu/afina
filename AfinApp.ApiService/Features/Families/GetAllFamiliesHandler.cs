using AfinApp.Data.Context;
using Mediator;
using Microsoft.EntityFrameworkCore;
using AfinApp.ApiService.Features.Families.Requests;

namespace AfinApp.ApiService.Features.Families;

public class GetAllFamiliesHandler(IFamilyRelevantContext ctx) : IRequestHandler<GetAllFamiliesRequest, IEnumerable<FamilyResponse>>
{
    public async ValueTask<IEnumerable<FamilyResponse>> Handle(GetAllFamiliesRequest req, CancellationToken ct)
        => await ctx.Families.Select(x => x.ConvertToResponse()).ToListAsync(ct);
}