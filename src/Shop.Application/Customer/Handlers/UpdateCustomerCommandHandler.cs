using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Shop.Application.Customer.Commands;
using Shop.Core.SharedKernel;
using Shop.Domain.Entities.CustomerAggregate;
using Shop.Domain.Entities.CustomerTypeAggregate;
using Shop.Domain.ValueObjects;

namespace Shop.Application.Customer.Handlers;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result>
{
    private readonly ICustomerWriteOnlyRepository _repository;
    private readonly ICustomerTypeWriteOnlyRepository _customerTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateCustomerCommand> _validator;

    public UpdateCustomerCommandHandler(
        ICustomerWriteOnlyRepository repository,
        ICustomerTypeWriteOnlyRepository customerTypeRepository,
        IUnitOfWork unitOfWork,
        IValidator<UpdateCustomerCommand> validator)
    {
        _repository = repository;
        _customerTypeRepository = customerTypeRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        // Validating the request.
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Returns the result with validation errors.
            return Result.Invalid(validationResult.AsErrors());
        }

        // Getting the customer from the database.
        var customer = await _repository
            .GetIncludeByIdAsync(request.Id);

        if (customer == null)
            return Result.NotFound($"No customer found by Id: {request.Id}");

        // Instantiating the Email value object.
        var emailResult = Email.Create(request.Email);
        if (!emailResult.IsSuccess)
            return Result.Error(emailResult.Errors.ToArray());

        // Checking if there is already a customer with the email address.
        if (await _repository.ExistsByEmailAsync(emailResult.Value, customer.Id))
            return Result.Error("The provided email address is already in use.");

        if (request.CustomerTypeId.HasValue)
        {
            var customerTypeResult = await _customerTypeRepository.GetByCustomerIdAsync(request.CustomerTypeId.Value);
            if (customerTypeResult == null)
            {
                return Result.NotFound($"The provided customer type id '{request.CustomerTypeId}' is not found.");
            }

            // Changing the email in the entity.
            customer.ChangeEmailAndCustomerType(emailResult.Value, customerTypeResult);
        } else
        {
            customer.ChangeEmail(emailResult.Value);
        }

        // Updating the entity in the repository.
        _repository.Update(customer);

        // Saving the changes to the database and firing events.
        await _unitOfWork.SaveChangesAsync();

        // Returning the success message.
        return Result.SuccessWithMessage("Updated successfully!");
    }
}