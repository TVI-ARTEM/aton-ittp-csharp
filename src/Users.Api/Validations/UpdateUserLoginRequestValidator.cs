using FluentValidation;
using Users.Api.Requests;

namespace Users.Api.Validations;

public class UpdateUserLoginRequestValidator : AbstractValidator<UpdateUserLoginRequest>
{
    private const string LoginRegex = @"^[a-zA-Z0-9]*$";

    public UpdateUserLoginRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty();
        RuleFor(x => x.NewLogin).Length(3, 20).Matches(LoginRegex);
        RuleFor(x => x.Token).NotEmpty();
    }
}