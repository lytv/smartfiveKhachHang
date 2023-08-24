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
public class DeleteCustomerTypeCommandHandler : IRequestHandler<DeleteCustomerTypeCommand, Result>
{
    private readonly ICustomerTypeWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteCustomerTypeCommand> _validator;

    public DeleteCustomerTypeCommandHandler(
        ICustomerTypeWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<DeleteCustomerTypeCommand> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result> Handle(DeleteCustomerTypeCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        var deleteCustomerType = await _repository.GetByIdAsync(request.Id);
        if (deleteCustomerType == null)
        {
            return Result.NotFound($"No customer type found by Id: {request.Id}");
        }

        deleteCustomerType.Delete();

        _repository.Remove(deleteCustomerType);

        // Saving the changes to the database and triggering the events.
        await _unitOfWork.SaveChangesAsync();

        return Result.SuccessWithMessage("Remove Successfully");
    }
}
