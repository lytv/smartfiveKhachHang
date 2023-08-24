using FluentValidation;

namespace Shop.Application.CustomerType.Commands;
public class DeleteCustomerTypeCommandValidator : AbstractValidator<DeleteCustomerTypeCommand>
{
    public DeleteCustomerTypeCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();
    }
}
