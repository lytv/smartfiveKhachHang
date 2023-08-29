using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities.CustomerTypeAggregate;
using Shop.Infrastructure.Data.Context;
using Shop.Infrastructure.Data.Repositories.Common;

namespace Shop.Infrastructure.Data.Repositories;
internal class CustomerTypeWriteOnlyRepository : BaseWriteOnlyRepository<CustomerType>, ICustomerTypeWriteOnlyRepository
{
    public CustomerTypeWriteOnlyRepository(WriteDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistByCodeAsync(string customerTypeCode) =>
        await Context.CustomerTypes
        .AsNoTracking()
        .AnyAsync(customerType => customerType.CustomerTypeCode == customerTypeCode);

    public async Task<bool> ExistByCodeAsync(string customerTypeCode, Guid currentId) =>
        await Context.CustomerTypes
        .AsNoTracking()
        .AnyAsync(customerType => customerType.CustomerTypeCode == customerTypeCode && customerType.Id == currentId);

    public async Task<CustomerType> GetByCustomerIdAsync(Guid id) =>
        await Context.CustomerTypes.FirstOrDefaultAsync(customerType => customerType.Id == id);
}
