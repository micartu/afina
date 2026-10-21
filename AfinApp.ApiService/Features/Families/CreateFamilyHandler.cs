using Mediator;
using AfinApp.Data.Shared;
using AfinApp.ApiService.Features.Families.Requests;
using FluentValidation;
using AfinApp.Data.Entities;
using AfinApp.Data.Context;

namespace AfinApp.ApiService.Features.Families;

public class CreateFamilyHandler(IFamilyRelevantContext ctx, IValidator<IFamilyRequest> val) : IRequestHandler<CreateFamilyRequest, Result<FamilyResponse>>
{
    public async ValueTask<Result<FamilyResponse>> Handle(CreateFamilyRequest req, CancellationToken ct)
    {
        if (!val.Validate(req).IsValid)
            return new Error(ErrorType.Validation, Const.apiFamilies, "Validation failed");
        var item = new Family
        {
            Id = Guid.NewGuid(),
            Name = req.Name
        };
        ctx.Families.Add(item);
        await ctx.SaveChangesAsync(ct);
        return item.ConvertToResponse();
    }
}