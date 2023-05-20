using FluentValidation;
using Users.Api.Requests;

namespace Users.Api.Validations;

public class UpdateUserPasswordRequestValidator : AbstractValidator<UpdateUserPasswordRequest>
{
    private const string PasswordRegex = @"^[a-zA-Z0-9]*$";

    public UpdateUserPasswordRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty();
        RuleFor(x => x.NewPassword).Length(3, 20).Matches(PasswordRegex);
        RuleFor(x => x.Token).NotEmpty();
    }
}