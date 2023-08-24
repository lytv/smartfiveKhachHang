using FluentValidation;

namespace Shop.Query.Application.CustomerType.Queries;
public class GetCustomerTypeByIdQueryValidator : AbstractValidator<GetCustomerTypeByIdQuery>
{
    public GetCustomerTypeByIdQueryValidator()
    {
        RuleFor(command => command.Id).NotEmpty();
    }
}
