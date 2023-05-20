using FluentValidation;
using Users.Api.Requests;

namespace Users.Api.Validations;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    private const string NameRegex = @"^[a-zA-ZЁёА-я]*$";

    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty();
        RuleFor(x => x.Name).Length(3, 20).Matches(NameRegex).When(it => it is not null);
        RuleFor(x => x.Gender).InclusiveBetween(0, 2).When(it => it is not null);
        RuleFor(x => x.Token).NotEmpty();
    }
}