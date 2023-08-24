using FluentValidation;

namespace Shop.Application.CustomerType.Commands;
public class CreateCustomerTypeCommandValidator : AbstractValidator<CreateCustomerTypeCommand>
{
    public CreateCustomerTypeCommandValidator()
    {
        RuleFor(command => command.CustomerTypeCode)
            .NotEmpty().MaximumLength(10);

        RuleFor(command => command.Description)
            .MaximumLength(255);

    }
}
