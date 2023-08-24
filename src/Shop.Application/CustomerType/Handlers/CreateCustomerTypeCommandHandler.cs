using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Shop.Application.CustomerType.Commands;
using Shop.Application.CustomerType.Responses;
using Shop.Core.SharedKernel;
using Shop.Domain.Entities.CustomerTypeAggregate;
using Shop.Domain.Factories;

namespace Shop.Application.CustomerType.Handlers;
public class CreateCustomerTypeCommandHandler : IRequestHandler<CreateCustomerTypeCommand, Result<CreatedCustomerTypeResponse>>
{
    private readonly ICustomerTypeWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateCustomerTypeCommand> _validator;

    public CreateCustomerTypeCommandHandler(
        ICustomerTypeWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<CreateCustomerTypeCommand> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<CreatedCustomerTypeResponse>> Handle(
        CreateCustomerTypeCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if(!validationResult.IsValid)
        {
            return Result<CreatedCustomerTypeResponse>.Invalid(validationResult.AsErrors());
        }

        if (await _repository.ExistByCodeAsync(request.CustomerTypeCode))
        {
            return Result<CreatedCustomerTypeResponse>.Error($"The provided {nameof(request.CustomerTypeCode)} is already in use.");
        }

        var customerType = CustomerTypeFactory.Create(request.CustomerTypeCode, request.Description, request.TenantId);

        _repository.Add(customerType);

        await _unitOfWork.SaveChangesAsync();

        return Result<CreatedCustomerTypeResponse>.Success(
            new CreatedCustomerTypeResponse(customerType.Id), "Successfully registered!");
    }
}
