using FluentValidation;
using Users.Api.Requests;

namespace Users.Api.Validations;

public class RestoreUserRequestValidator : AbstractValidator<RestoreUserRequest>
{
    public RestoreUserRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty();
        RuleFor(x => x.Token).NotEmpty();
    }
}