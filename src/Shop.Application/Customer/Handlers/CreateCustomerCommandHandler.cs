using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Shop.Application.Customer.Commands;
using Shop.Application.Customer.Responses;
using Shop.Core.SharedKernel;
using Shop.Domain.Entities.CustomerAggregate;
using Shop.Domain.Entities.CustomerTypeAggregate;
using Shop.Domain.Factories;
using Shop.Domain.ValueObjects;

namespace Shop.Application.Customer.Handlers;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<CreatedCustomerResponse>>
{
    private readonly ICustomerWriteOnlyRepository _repository;
    private readonly ICustomerTypeWriteOnlyRepository _customerTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateCustomerCommand> _validator;

    public CreateCustomerCommandHandler(
        ICustomerWriteOnlyRepository repository,
        ICustomerTypeWriteOnlyRepository customerTypeRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateCustomerCommand> validator)
    {
        _repository = repository;
        _customerTypeRepository = customerTypeRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<CreatedCustomerResponse>> Handle(
        CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        // Validating the request.
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Return the result with validation errors.
            return Result<CreatedCustomerResponse>.Invalid(validationResult.AsErrors());
        }

        // Instantiating the Email value object.
        var emailResult = Email.Create(request.Email);
        if (!emailResult.IsSuccess)
            return Result<CreatedCustomerResponse>.Error(emailResult.Errors.ToArray());

        // Checking if a customer with the email address already exists.
        if (await _repository.ExistsByEmailAsync(emailResult.Value))
            return Result<CreatedCustomerResponse>.Error("The provided email address is already in use.");

        var customerTypeResult = await _customerTypeRepository.GetByIdAsync(request.CustomerTypeId);
        if (customerTypeResult == null)
        {
            return Result<CreatedCustomerResponse>.NotFound("The provided customer type id is not found.");
        }


        // Creating an instance of the customer entity.
        // When instantiated, the "CustomerCreatedEvent" will be created.
        var customer = CustomerFactory.Create(
            request.FirstName,
            request.LastName,
            request.Gender,
            emailResult.Value,
            request.DateOfBirth,
            customerTypeResult,
            request.TenantId);

        // Adding the entity to the repository.
        _repository.Add(customer);

        // Saving changes to the database and triggering events.
        await _unitOfWork.SaveChangesAsync();

        // Returning the ID and success message.
        return Result<CreatedCustomerResponse>.Success(
            new CreatedCustomerResponse(customer.Id), "Successfully registered!");
    }
}