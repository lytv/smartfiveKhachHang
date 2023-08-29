using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities.CustomerAggregate;
using Shop.Domain.ValueObjects;
using Shop.Infrastructure.Data.Context;
using Shop.Infrastructure.Data.Repositories.Common;

namespace Shop.Infrastructure.Data.Repositories;

internal class CustomerWriteOnlyRepository : BaseWriteOnlyRepository<Customer>, ICustomerWriteOnlyRepository
{
    public CustomerWriteOnlyRepository(WriteDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsByEmailAsync(Email email) =>
        await Context.Customers
            .AsNoTracking()
            .AnyAsync(customer => customer.Email.Address == email.Address);

    public async Task<bool> ExistsByEmailAsync(Email email, Guid currentId) =>
        await Context.Customers
            .AsNoTracking()
            .AnyAsync(customer => customer.Email.Address == email.Address && customer.Id != currentId);

    public async Task<Customer> GetIncludeByIdAsync(Guid id) =>
        await Context.Customers
        .Include(customer => customer.CustomerType)
        .FirstOrDefaultAsync(customer => customer.Id == id);
}