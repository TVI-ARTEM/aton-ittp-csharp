using System.Text.RegularExpressions;
using FluentValidation;
using Users.Api.Requests;

namespace Users.API.Validations;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    private const string LoginRegex = "^[a-zA-Z0-9]+$";
    private const string PasswordRegex = "^[a-zA-Z0-9]+$";
    private const string NameRegex = "^[a-zA-ZЁёА-я]+$";

    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty().Matches(LoginRegex);
        RuleFor(x => x.Password).NotEmpty().Matches(PasswordRegex);
        RuleFor(x => x.Name).NotEmpty().Matches(NameRegex);

        RuleFor(x => x.Gender).InclusiveBetween(0, 2);
    }
}