using FluentValidation;
using Users.Api.Requests;

namespace Users.Api.Validations;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    private const string LoginRegex = @"^[a-zA-Z0-9]*$";
    private const string PasswordRegex = @"^[a-zA-Z0-9]*$";
    private const string NameRegex = @"^[a-zA-ZЁёА-я]*$";

    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty().Length(3, 20).Matches(LoginRegex);
        RuleFor(x => x.Password).NotEmpty().Length(3, 20).Matches(PasswordRegex);
        RuleFor(x => x.Name).NotEmpty().Length(3, 20).Matches(NameRegex);
        RuleFor(x => x.Gender).InclusiveBetween(0, 2);
        RuleFor(x => x.Token).NotEmpty();
    }
}