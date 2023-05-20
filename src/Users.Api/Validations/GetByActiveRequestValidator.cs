using FluentValidation;
using Users.Api.Requests;

namespace Users.Api.Validations;

public class GetByActiveRequestValidator : AbstractValidator<GetByActiveRequest>
{
    public GetByActiveRequestValidator()
    {
        RuleFor(x => x.Take).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Token).NotEmpty();
    }
}