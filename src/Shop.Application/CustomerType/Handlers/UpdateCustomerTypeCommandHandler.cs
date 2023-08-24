using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Shop.Application.CustomerType.Commands;
using Shop.Core.SharedKernel;
using Shop.Domain.Entities.CustomerTypeAggregate;

namespace Shop.Application.CustomerType.Handlers;
public class UpdateCustomerTypeCommandHandler : IRequestHandler<UpdateCustomerTypeCommand, Result>
{

    private readonly ICustomerTypeWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateCustomerTypeCommand> _validator;

    public UpdateCustomerTypeCommandHandler(
        ICustomerTypeWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<UpdateCustomerTypeCommand> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result> Handle(UpdateCustomerTypeCommand request, CancellationToken cancellationToken)
    {
        // validate the request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            // Returns the result with validation errors.
            return Result.Invalid(validationResult.AsErrors());
        }

        var customerType = await _repository.GetByIdAsync(request.Id);
        if (customerType == null)
        {
            return Result.NotFound($"No Customer Type found by Id: {request.Id}");
        }

        var customerTypeCodeResult = await _repository.ExistByCodeAsync(request.CustomerTypeCode);
        if (customerTypeCodeResult)
        {
            return Result.Error($"Customer Type Code {request.CustomerTypeCode} already exists!");
        }

        customerType.ChangeCodeAndDescription(request.CustomerTypeCode, request.Description);

        // Updating the entity in the repository.
        _repository.Update(customerType);

        // Saving the changes to the database and firing events.
        await _unitOfWork.SaveChangesAsync();

        // Returning the success message.
        return Result.SuccessWithMessage("Updated successfully!");

    }
}
