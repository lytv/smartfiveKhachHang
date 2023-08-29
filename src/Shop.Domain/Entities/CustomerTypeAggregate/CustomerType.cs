using System.Collections.Generic;
using Shop.Core.SharedKernel;
using Shop.Domain.Entities.CustomerAggregate;
using Shop.Domain.Entities.CustomerTypeAggregate.Events;

namespace Shop.Domain.Entities.CustomerTypeAggregate;
public class CustomerType : BaseEntity, IAggregateRoot
{
    private bool _isDeleted;

    public CustomerType(string customerTypeCode, string description, int tenantId = 1)
    {
        CustomerTypeCode = customerTypeCode;
        Description = description;
        TenantId = tenantId;

        AddDomainEvent(new CustomerTypeCreatedEvent(Id, customerTypeCode, description, tenantId));
    }

    public CustomerType()
    {
    }

    public string CustomerTypeCode { get; set; }
    public string Description { get; set; }
    public int TenantId { get; set; } = 1;
    public List<Customer> Customers { get; set; }

    public void ChangeCodeAndDescription(string customerTypeCode, string description)
    {
        if(CustomerTypeCode.Equals(customerTypeCode) || Description.Equals(description))
            return;

        CustomerTypeCode = customerTypeCode;
        Description = description;

        AddDomainEvent(new CustomerTypeUpdatedEvent(Id, customerTypeCode, description, TenantId));
    }

    public void Delete()
    {
        if (_isDeleted) return;

        _isDeleted = true;

        AddDomainEvent(new CustomerTypeDeletedEvent(Id, CustomerTypeCode, Description, TenantId));
    }
}
