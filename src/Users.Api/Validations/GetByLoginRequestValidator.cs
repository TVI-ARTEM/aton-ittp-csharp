using FluentValidation;
using Users.Api.Requests;

namespace Users.Api.Validations;

public class GetByLoginRequestValidator : AbstractValidator<GetByLoginRequest>
{

    public GetByLoginRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty();
        RuleFor(x => x.Token).NotEmpty();
    }
}