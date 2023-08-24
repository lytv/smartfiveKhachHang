using Shop.Domain.Entities.CustomerTypeAggregate;

namespace Shop.Domain.Factories;
public static class CustomerTypeFactory
{
    public static CustomerType Create(
        string customerTypeCode,
        string description,
        int tenantId) => new(customerTypeCode, description, tenantId);
}
