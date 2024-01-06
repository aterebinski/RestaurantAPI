using FluentValidation;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Models.Validators
{
    public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserDTOValidator(RestaurantDbContext dbContext)
        {
            RuleFor(i=>i.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(i => i.Password)
                .NotEmpty()
                .MinimumLength(10);

            RuleFor(i=>i.ConfirmPassword)
                .Equal(j=>j.Password);

            RuleFor(i => i.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken.");
                    }
                });
        }
    }
}
