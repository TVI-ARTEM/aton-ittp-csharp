using FluentValidation;
using Users.Api.Requests;

namespace Users.Api.Validations;

public class GetByAgeRequestValidator : AbstractValidator<GetByAgeRequest>
{
    public GetByAgeRequestValidator()
    {
        RuleFor(x => x.Age).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Take).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Token).NotEmpty();
    }
}