using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Shop.Core.SharedKernel;
using Shop.Query.Application.Customer.Queries;
using Shop.Query.Application.CustomerType.Queries;
using Shop.Query.Data.Repositories.Abstractions;
using Shop.Query.QueriesModel;

namespace Shop.Query.Application.CustomerType.Handlers;
public class GetAllCustomerTypeQueryHandler :
    IRequestHandler<GetAllCustomerTypeQuery, Result<IEnumerable<CustomerTypeQueryModel>>>
{
    private const string CacheKey = nameof(GetAllCustomerTypeQuery);
    private readonly ICacheService _cacheService;
    private readonly ICustomerTypeReadOnlyRepository _readOnlyRepository;

    public GetAllCustomerTypeQueryHandler(ICacheService cacheService, ICustomerTypeReadOnlyRepository readOnlyRepository)
    {
        _cacheService = cacheService;
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task<Result<IEnumerable<CustomerTypeQueryModel>>> Handle(GetAllCustomerTypeQuery request, CancellationToken cancellationToken)
    {
        return Result<IEnumerable<CustomerTypeQueryModel>>.Success(
           await _cacheService.GetOrCreateAsync(CacheKey, _readOnlyRepository.GetAllAsync));
    }
}
