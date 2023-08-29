using System;
using System.Threading.Tasks;
using Shop.Core.SharedKernel;

namespace Shop.Domain.Entities.CustomerTypeAggregate;
public interface ICustomerTypeWriteOnlyRepository : IWriteOnlyRepository<CustomerType>
{
    /// <summary>
    /// Checks if a customer type with the specified customer type code already exists asynchronously.
    /// </summary>
    /// <param name="customerTypeCode">The customer type code to check.</param>
    /// <returns>True if a customer type with the customer type code exists, false otherwise.</returns>
    Task<bool> ExistByCodeAsync(string customerTypeCode);

    /// <summary>
    /// Checks if a customer type with the specified customer type code and current ID already exists asynchronously.
    /// </summary>
    /// <param name="customerTypeCode">The customer type code to check.</param>
    /// <param name="currentId">The current ID of the customer type to exclude from the check.</param>
    /// <returns>True if a customer type with the customer type code and current ID exists, false otherwise.</returns>
    Task<bool> ExistByCodeAsync(string customerTypeCode, Guid currentId);

    Task<CustomerType> GetByCustomerIdAsync(Guid id);
}
