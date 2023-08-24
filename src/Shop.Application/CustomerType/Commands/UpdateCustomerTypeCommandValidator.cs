using FluentValidation;

namespace Shop.Application.CustomerType.Commands;
public class UpdateCustomerTypeCommandValidator : AbstractValidator<UpdateCustomerTypeCommand>
{
    public UpdateCustomerTypeCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();

        RuleFor(command => command.CustomerTypeCode).MaximumLength(10).NotEmpty();

        RuleFor(command => command.Description).MaximumLength(255).NotEmpty();
    }
}
