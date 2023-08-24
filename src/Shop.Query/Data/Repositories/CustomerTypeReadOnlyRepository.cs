using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Shop.Query.Abstractions;
using Shop.Query.Data.Repositories.Abstractions;
using Shop.Query.QueriesModel;

namespace Shop.Query.Data.Repositories;
internal class CustomerTypeReadOnlyRepository : BaseReadOnlyRepository<CustomerTypeQueryModel>, ICustomerTypeReadOnlyRepository
{
    public CustomerTypeReadOnlyRepository(IReadDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CustomerTypeQueryModel>> GetAllAsync() =>
        await Collection
            .Find(Builders<CustomerTypeQueryModel>.Filter.Empty)
            .SortBy(customer => customer.CustomerTypeCode)
            .ThenBy(customer => customer.TenantId)
            .ToListAsync();
}
