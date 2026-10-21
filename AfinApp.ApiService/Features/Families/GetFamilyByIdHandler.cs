using Mediator;
using AfinApp.Data.Shared;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using AfinApp.ApiService.Features.Families.Requests;
using AfinApp.Data.Context;

namespace AfinApp.ApiService.Features.Families;

public class GetFamilyByIdHandler(IFamilyRelevantContext ctx) : IRequestHandler<GetFamilyByIdRequest, Result<FamilyResponse>>
{
    public async ValueTask<Result<FamilyResponse>> Handle(GetFamilyByIdRequest req, CancellationToken ct)
    {
        var item = await ctx.Families.Where(x => x.Id == req.Id).Select(x => x.ConvertToResponse()).FirstOrDefaultAsync(ct);
        return item != null ? item : new Error(ErrorType.NotFound, $"{Const.apiFamilies}.NotFound", "Not found");
    }
}