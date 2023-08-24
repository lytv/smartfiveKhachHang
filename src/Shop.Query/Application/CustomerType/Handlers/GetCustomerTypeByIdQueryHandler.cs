using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Shop.Core.SharedKernel;
using Shop.Query.Application.CustomerType.Queries;
using Shop.Query.Data.Repositories.Abstractions;
using Shop.Query.QueriesModel;

namespace Shop.Query.Application.CustomerType.Handlers;
public class GetCustomerTypeByIdQueryHandler : IRequestHandler<GetCustomerTypeByIdQuery, Result<CustomerTypeQueryModel>>
{
    private readonly ICacheService _cacheService;
    private readonly ICustomerTypeReadOnlyRepository _repository;
    private readonly IValidator<GetCustomerTypeByIdQuery> _validator;

    public GetCustomerTypeByIdQueryHandler(ICacheService cacheService, ICustomerTypeReadOnlyRepository readOnlyRepository, IValidator<GetCustomerTypeByIdQuery> validator)
    {
        _cacheService = cacheService;
        _repository = readOnlyRepository;
        _validator = validator;
    }

    public async Task<Result<CustomerTypeQueryModel>> Handle(GetCustomerTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<CustomerTypeQueryModel>.Invalid(validationResult.AsErrors());
        }

        // Creating a cache key using the query name and the customer ID.
        var cacheKey = $"{nameof(GetCustomerTypeByIdQuery)}_{request.Id}";

        // Getting the customer type from the cache service. If not found, fetches it from the repository.
        // The customer type will be stored in the cache service for future queries.
        var customerType = await _cacheService.GetOrCreateAsync(cacheKey, () => _repository.GetByIdAsync(request.Id));

        // If the customerType is null, returns a result indicating that no customer type was found.
        // Otherwise, returns a successful result with the customer type.
        return customerType == null
            ? Result<CustomerTypeQueryModel>.NotFound($"No customer type found by ID: {request.Id}")
            : Result<CustomerTypeQueryModel>.Success(customerType);
    }
}
