using FluentValidation;
using Users.Api.Requests;

namespace Users.Api.Validations;

public class AuthRequestValidator : AbstractValidator<AuthRequest>
{
    private const string LoginRegex = @"^[a-zA-Z0-9]*$";
    private const string PasswordRegex = @"^[a-zA-Z0-9]*$";

    public AuthRequestValidator()
    {
        RuleFor(x => x.Login).Length(3, 20).Matches(LoginRegex).When(it => it is not null);
        RuleFor(x => x.Password).Length(3, 20).Matches(PasswordRegex).When(it => it is not null);
    }
}