using Mediator;
using AfinApp.Data.Shared;
using AfinApp.Data.Context;
using AfinApp.ApiService.Features.Families.Requests;

namespace AfinApp.ApiService.Features.Families;

public class DeleteFamilyHandler(IFamilyRelevantContext ctx) : IRequestHandler<DeleteFamilyRequest, Result>
{
    public async ValueTask<Result> Handle(DeleteFamilyRequest req, CancellationToken ct)
    {
        var item = await ctx.Families.FindAsync([req.Id], ct);
        if (item == null)
            return new Error(ErrorType.NotFound, $"{Const.apiFamilies}.NotFound", "Not found");
        ctx.Families.Remove(item);
        await ctx.SaveChangesAsync(ct);
        return Result.Success();
    }
}