using Mediator;
using AfinApp.Data.Shared;
using AfinApp.ApiService.Features.Families.Requests;
using FluentValidation;
using AfinApp.Data.Context;

namespace AfinApp.ApiService.Features.Families;

public class UpdateFamilyHandler(IFamilyRelevantContext ctx, IValidator<IFamilyRequest> val) : IRequestHandler<UpdateFamilyRequest, Result<FamilyResponse>>
{
    public async ValueTask<Result<FamilyResponse>> Handle(UpdateFamilyRequest req, CancellationToken ct)
    {
        if (!val.Validate(req).IsValid)
            return new Error(ErrorType.Validation, Const.apiFamilies, "Validation failed");
        var item = await ctx.Families.FindAsync([req.Id], ct);
        if (item == null)
            return new Error(ErrorType.NotFound, $"{Const.apiFamilies}.NotFound", "Not found");
        item.Name = req.Name;
        await ctx.SaveChangesAsync(ct);
        return item.ConvertToResponse();
    }
}